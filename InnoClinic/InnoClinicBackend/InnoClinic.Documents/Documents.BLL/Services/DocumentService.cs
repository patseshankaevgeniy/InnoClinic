using AutoMapper;
using Documents.BLL.Constants;
using Documents.BLL.Models;
using Documents.BLL.Services.Interfaces;
using Documents.DAL.Common.Constants;
using Documents.DAL.Common.Enums;
using Documents.DAL.Entities;
using Documents.DAL.Repositories.Interfaces;
using Minio;
using Minio.DataModel.Args;

namespace Documents.BLL.Services;

public class DocumentService(
    IDocumentRepository repository,
    IMinioClient minioClient,
    IMapper mapper) : IDocumentService
{
    public async Task<DocumentModel> UploadAsync(Stream fileStream, string fileName, Guid identityId, DocumentType documentType, CancellationToken cancellationToken)
    {
        if (fileStream is null)
            throw new ArgumentNullException(nameof(fileStream), ErrorMessage.StreamIsNull);

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException(ErrorMessage.FileNameIsRequired, nameof(fileName));

        if (!fileStream.CanRead)
            throw new ArgumentException(ErrorMessage.StreamNotReadable, nameof(fileStream));

        Stream uploadStream = fileStream;
        MemoryStream? tempStream = null;
        try
        {
            long length;
            try
            {
                length = uploadStream.Length;
            }
            catch (NotSupportedException)
            {
                tempStream = new MemoryStream();
                await fileStream.CopyToAsync(tempStream, cancellationToken);
                length = tempStream.Length;
                if (length == 0)
                    throw new ArgumentException(ErrorMessage.StreamIsEmpty, nameof(fileStream));
                tempStream.Position = 0;
                uploadStream = tempStream;
            }

            if (uploadStream.CanSeek && uploadStream.Length == 0)
                throw new ArgumentException(ErrorMessage.StreamIsEmpty, nameof(fileStream));

            var fileKey = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
            var bucket = DocumentConstants.StorageBucket;

            await minioClient.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucket)
                .WithObject(fileKey)
                .WithStreamData(uploadStream)
                .WithObjectSize(uploadStream.CanSeek ? uploadStream.Length : tempStream!.Length)
                .WithContentType(documentType.GetMimeType()));

            var document = new Document
            {
                IdentityId = identityId,
                FileName = fileName,
                ContentType = documentType,
                FileKey = fileKey,
            };

            await repository.AddAsync(document, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);

            return mapper.Map<DocumentModel>(document);
        }
        finally
        {
            tempStream?.Dispose();
        }
    }

    public async Task<(byte[] Content, DocumentType ContentType)> GetDocumentAsync(Guid documentId, CancellationToken cancellationToken)
    {
        var meta = await repository.GetByIdAsync(documentId, cancellationToken);
        if (meta == null) throw new FileNotFoundException(ErrorMessage.FileNotFound);

        var bucket = DocumentConstants.StorageBucket;

        using MemoryStream memoryStream = new MemoryStream();
        await minioClient.GetObjectAsync(new GetObjectArgs()
            .WithBucket(bucket)
            .WithObject(meta.FileKey)
            .WithCallbackStream(stream => stream.CopyTo(memoryStream))
        );

        return (memoryStream.ToArray(), meta.ContentType);
    }

    public async Task<IEnumerable<DocumentModel>> GetByIdentityIdAsync(Guid identityId, CancellationToken cancellationToken)
    {
        var docs = await repository.GetByIdentityIdAsync(identityId, cancellationToken);
        return mapper.Map<IEnumerable<DocumentModel>>(docs);
    }

    public async Task DeleteAsync(Guid documentId, CancellationToken cancellationToken)
    {
        var meta = await repository.GetByIdAsync(documentId, cancellationToken);
        if (meta == null) throw new FileNotFoundException(ErrorMessage.FileNotFound);

        var bucket = DocumentConstants.StorageBucket;

        await minioClient.RemoveObjectAsync(new RemoveObjectArgs()
            .WithBucket(bucket)
            .WithObject(meta.FileKey));

        repository.Delete(meta);
        await repository.SaveChangesAsync(cancellationToken);
    }
}
