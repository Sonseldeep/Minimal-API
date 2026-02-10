namespace Movie.Api.ImageUploader;

public interface IImageUploader
{
    Task<string> UploadAsync(
        IFormFile file,
        CancellationToken cancellationToken);
}