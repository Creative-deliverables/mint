using OpenAI.Images;

namespace ShareInvest.Agency.Models;

public record ImageGenerationRequest(string UserId, string Path, string SceneId, string Prompt, string AspectRatio, GeneratedImageQuality? Quality = null, string? SessionId = null);