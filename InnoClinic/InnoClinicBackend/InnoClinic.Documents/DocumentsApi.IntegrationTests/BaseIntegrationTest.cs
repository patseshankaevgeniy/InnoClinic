using Documents.API.Dtos;
using Documents.DAL;
using Documents.DAL.Common.Constants;
using Documents.DAL.Common.Enums;
using Documents.DAL.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Minio;
using Minio.DataModel.Args;

namespace DocumentsApi.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestFactory>
{
    protected readonly IntegrationTestFactory Factory;
    protected readonly HttpClient Client;

    protected BaseIntegrationTest(IntegrationTestFactory factory)
    {
        Factory = factory;
        Client = factory.CreateClient();
    }

    protected async Task AddToDatabaseAsync<T>(T entity) where T : class
    {
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Set<T>().Add(entity);
        await db.SaveChangesAsync();
    }

    protected MultipartFormDataContent ToMultipartFormDataContent(CreatedDocumentDto dto)
    {
        var content = new MultipartFormDataContent();

        var stream = dto.File.OpenReadStream();
        var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(dto.File.ContentType);
        content.Add(fileContent, "File", dto.File.FileName);

        content.Add(new StringContent(dto.IdentityId.ToString()), nameof(dto.IdentityId));
        content.Add(new StringContent(dto.FileName), nameof(dto.FileName));
        content.Add(new StringContent(((int)dto.ContentType).ToString()), nameof(dto.ContentType));

        return content;
    }

    protected async Task SeedDocumentAsync(Guid id, string fileKey, byte[] content)
    {
        var bucket = DocumentConstants.StorageBucket;
        using var scope = Factory.Services.CreateScope();
        var minio = scope.ServiceProvider.GetRequiredService<IMinioClient>();

        var bexists = await minio.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucket));
        if (!bexists) await minio.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucket));

        var document = new Document
        {
            Id = id,
            IdentityId = Guid.NewGuid(),
            FileName = "test.jpg",
            ContentType = DocumentType.Jpg,
            FileKey = fileKey,
            CreatedAt = DateTime.UtcNow
        };
        await AddToDatabaseAsync(document);

        using var ms = new MemoryStream(content);
        await minio.PutObjectAsync(new PutObjectArgs()
            .WithBucket(bucket).WithObject(fileKey)
            .WithStreamData(ms).WithObjectSize(content.Length));
    }

    protected async Task AssertDocumentIsDeleted(Guid id)
    {
        using var scope = Factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var exists = await db.Documents.AnyAsync(x => x.Id == id);
        exists.Should().BeFalse($"Document {id} should have been deleted from DB");
    }
}
