using AutoMapper;
using Documents.API.Common;
using Documents.API.Dtos;
using Documents.BLL.Models;
using Documents.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Documents.API.Controllers;

[ApiController]
[Route(RouteConstants.DocumentsControllerRoute)]
public class DocumentsController(IDocumentService documentService, IMapper mapper) : ControllerBase
{
    [HttpGet(RouteConstants.GetByIdRoute)]
    [ProducesResponseType(typeof(FileContentHttpResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IResult> GetPhoto(Guid id, CancellationToken cancellationToken = default)
    {
        var (content, contentType) = await documentService.GetDocumentAsync(id, cancellationToken);

        return TypedResults.Bytes(content, contentType.GetMimeType());
    }

    [HttpPost(RouteConstants.CreateRoute)]
    [ProducesResponseType(typeof(DocumentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Upload([FromForm] CreatedDocumentDto createdDocument, CancellationToken cancellationToken = default)
    {
        using var stream = createdDocument.File.OpenReadStream();

        var resultModel = await documentService.UploadAsync(
            stream,
            createdDocument.FileName,
            createdDocument.IdentityId,
            createdDocument.ContentType,
            cancellationToken);

        var dto = mapper.Map<DocumentDto>(resultModel);

        return Ok(dto);
    }

    [HttpGet(RouteConstants.GetByIdentityIdRoute)]
    [ProducesResponseType(typeof(IEnumerable<DocumentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdentity(Guid identityId, CancellationToken cancellationToken = default)
    {
        var items = await documentService.GetByIdentityIdAsync(identityId, cancellationToken);

        var dtos = mapper.Map<IEnumerable<DocumentDto>>(items);

        return Ok(dtos);
    }

    [HttpDelete(RouteConstants.DeleteRoute)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        await documentService.DeleteAsync(id, cancellationToken);

        return NoContent();
    }
}
