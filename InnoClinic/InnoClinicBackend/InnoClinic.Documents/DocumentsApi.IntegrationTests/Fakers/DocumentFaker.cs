using Bogus;
using Documents.API.Dtos;
using Documents.DAL.Common.Enums;
using Documents.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace DocumentsApi.IntegrationTests.Fakers;

public static class DocumentFaker
{
    private static readonly Faker<Document> EntityFaker = new Faker<Document>()
        .RuleFor(d => d.Id, f => f.Random.Guid())
        .RuleFor(d => d.IdentityId, f => f.Random.Guid())
        .RuleFor(d => d.FileName, f => f.System.FileName("jpg"))
        .RuleFor(d => d.ContentType, f => DocumentType.Jpg)
        .RuleFor(d => d.FileKey, f => $"documents/{Guid.NewGuid()}.jpg")
        .RuleFor(d => d.CreatedAt, f => DateTime.UtcNow);

    public static CreatedDocumentDto GenerateDto()
    {
        var f = new Faker();
        var content = "fake image content"u8.ToArray();
        var stream = new MemoryStream(content);
        var fileName = f.System.FileName("jpg");

        return new CreatedDocumentDto
        {
            File = new FormFile(stream, 0, content.Length, "File", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            },
            IdentityId = f.Random.Guid(),
            FileName = fileName,
            ContentType = DocumentType.Jpg
        };
    }

    public static Document GenerateEntity(Guid? id = null)
    {
        return EntityFaker.Clone()
            .RuleFor(d => d.Id, f => id ?? f.Random.Guid())
            .Generate();
    }

    public static (Guid Id, string Key, byte[] Content) GenerateSeedData()
    {
        var entity = GenerateEntity();
        return (entity.Id, entity.FileKey, "hello world"u8.ToArray());
    }
}
