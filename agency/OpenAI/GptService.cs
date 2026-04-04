using Microsoft.Extensions.Logging;

using OpenAI;
using OpenAI.Images;

using ShareInvest.Agency.Models;

using System.ClientModel;

#pragma warning disable OPENAI001

namespace ShareInvest.Agency.OpenAI;

public class GptService : OpenAIClient
{
    public GptService(ILogger<GptService> logger, string apiKey) : base(apiKey)
    {
        this.logger = logger;
    }

    public GptService(ILogger<GptService> logger, string apiKey, string imageModel) : base(apiKey)
    {
        this.logger = logger;
        this.imageModel = imageModel;
    }

    readonly ILogger<GptService> logger;
    readonly string? imageModel;

    public async Task<T?> GenerateImageAsync<T>(ImageGenerationRequest request, CancellationToken cancellationToken = default) where T : class
    {
        var size = MapSize(request.AspectRatio);

        var imageClient = GetImageClient(imageModel);

        var options = new ImageGenerationOptions
        {
            Size = size,
            Quality = request.Quality ?? GeneratedImageQuality.HighQuality,
            OutputFileFormat = GeneratedImageFileFormat.Png,
        };
        ClientResult<GeneratedImage> result;

        try
        {
            result = await imageClient.GenerateImageAsync(request.Prompt, options, cancellationToken);

            return result.Value.ImageBytes as T;
        }
        catch (ClientResultException ex) when (ex.Status == 400)
        {
            logger.LogWarning(ex, "Image generation blocked: {Message}", ex.Message);

            throw new ImageGenerationModerationException(ex.Message);
        }
    }

    static GeneratedImageSize MapSize(string aspectRatio) => aspectRatio switch
    {
        "9:16" => GeneratedImageSize.W1024xH1536,
        "16:9" => GeneratedImageSize.W1536xH1024,
        _ => GeneratedImageSize.W1024xH1024,
    };
}