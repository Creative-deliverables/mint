namespace ShareInvest.Agency.Models;

/// <summary>
/// Thrown when OpenAI's safety system blocks an image generation request.
/// </summary>
public class ImageGenerationModerationException : Exception
{
    public ImageGenerationModerationException(string message) : base(message)
    {

    }

    public ImageGenerationModerationException(string message, Exception innerException) : base(message, innerException)
    {

    }
}