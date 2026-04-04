# ShareInvest.Agency

AI agency library for PageMint — multi-provider text and image generation services.

## Packages

| Package | Description |
|---------|-------------|
| `ShareInvest.Agency` | Core interfaces and AI service implementations |

## Installation

```bash
dotnet add package ShareInvest.Agency --source https://nuget.pkg.github.com/cyberprophet/index.json
```

## Usage

```csharp
// Register in DI
services.AddSingleton(new GptService(logger, apiKey, imageModel));

// Inject and use
public class MyService(GptService gpt)
{
    public async Task GenerateAsync()
    {
        var result = await gpt.GenerateImageAsync<BinaryData>(request);
    }
}
```

## License

MIT
