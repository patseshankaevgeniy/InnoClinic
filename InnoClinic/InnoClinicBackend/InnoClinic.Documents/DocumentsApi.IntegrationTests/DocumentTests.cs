using DocumentsApi.IntegrationTests.Fakers;
using FluentAssertions;
using System.Net;

namespace DocumentsApi.IntegrationTests;

public class DocumentTests(IntegrationTestFactory factory) : BaseIntegrationTest(factory)
{
    private const string BaseUrl = "api/documents";

    [Fact]
    public async Task Upload_ValidFile_ShouldReturnOk()
    {
        // Arrange
        var dto = DocumentFaker.GenerateDto();
        using var content = ToMultipartFormDataContent(dto);

        // Act
        var response = await Client.PostAsync($"{BaseUrl}/create", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task GetById_IfExist_ShouldReturnOk()
    {
        // Arrange
        var (id, key, content) = DocumentFaker.GenerateSeedData();
        await SeedDocumentAsync(id, key, content);

        // Act
        var response = await Client.GetAsync($"{BaseUrl}/get-by-id?Id={id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (await response.Content.ReadAsByteArrayAsync()).Should().BeEquivalentTo(content);
    }

    [Fact]
    public async Task Delete_IfExist_ShouldReturnNoContent()
    {
        // Arrange
        var (id, key, content) = DocumentFaker.GenerateSeedData();
        await SeedDocumentAsync(id, key, content);

        // Act
        var response = await Client.DeleteAsync($"{BaseUrl}/delete?Id={id}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        await AssertDocumentIsDeleted(id);
    }

    [Fact]
    public async Task Delete_IfNotExist_ShouldReturnNotFound()
    {
        // Act
        var response = await Client.DeleteAsync($"{BaseUrl}/{Guid.NewGuid()}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}