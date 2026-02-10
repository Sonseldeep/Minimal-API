
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Movie.Api.ImageUploader;
public class ImageUploader : IImageUploader
{
    private readonly BlobContainerClient _containerClient;

    public ImageUploader(IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:AzureStorage"]
                               ?? throw new InvalidOperationException("AzureStorage connection string is missing.");
        var containerName = configuration["AzureStorage:ContainerName"]
                            ?? throw new InvalidOperationException("AzureStorage container name is missing.");

        _containerClient = new BlobContainerClient(connectionString, containerName);

      
        _containerClient.CreateIfNotExists(PublicAccessType.Blob);
    }
    public async Task<string> UploadAsync(
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var fileName =
            $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

        var blobClient =
            _containerClient.GetBlobClient(fileName);

        await using var stream = file.OpenReadStream();

        await blobClient.UploadAsync(
            stream,
            new BlobHttpHeaders
            {
                ContentType = file.ContentType
            },
            cancellationToken: cancellationToken);

        return blobClient.Uri.ToString();
    }
}
