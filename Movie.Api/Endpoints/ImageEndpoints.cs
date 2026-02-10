using Microsoft.AspNetCore.Mvc;
using Movie.Api.Contracts.Requests;
using Movie.Api.Contracts.Responses;
using Movie.Api.ImageUploader;

namespace Movie.Api.Endpoints;

public static class ImageEndpoints
{
    private const long MaxFileSize = 5 * 1024 * 1024;
    
    private static readonly string[] AllowedImageTypes =
    [
        "image/jpeg",
        "image/jpg", 
        "image/png",
        "image/gif",
        "image/bmp",
        "image/webp"
    ];

    public static void MapImageEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/images");
        
        group.MapPost("/", async (
                [FromForm] ImageUploadRequest request,
                IImageUploader uploader,
                CancellationToken ct
            ) =>
            {
                var file = request.File;

                if (file.Length <= 0)
                {
                    return Results.BadRequest("File is required.");
                }


                if (file.Length > MaxFileSize)
                {
                    return Results.BadRequest("File size exceeds 5MB limit.");
                }
                    

                if (!AllowedImageTypes.Contains(
                        file.ContentType, 
                        StringComparer.OrdinalIgnoreCase))
                    return Results.BadRequest("Invalid file type.");

                try
                {
                    var blobUri = await uploader.UploadAsync(file, ct);
                    return Results.Ok(new ImageUploadResponse(blobUri));
                }
                
                catch (Exception)
                {
                    return Results.Problem("Failed to upload image.");
                }
            })
            .RequireAuthorization(policy => policy.RequireRole("Admin", "SuperAdmin"))
            .Accepts<ImageUploadRequest>("multipart/form-data")
            .Produces<ImageUploadResponse>()
            .DisableAntiforgery();
    }
}