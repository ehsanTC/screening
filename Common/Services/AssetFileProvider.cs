using System.Reflection;
using Common.Abstraction;

namespace Common.Services;

public class AssetFileProvider : IAssetFileProvider
{
    public Stream OpenAsset(string path)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var stream = assembly.GetManifestResourceStream(path);
        
        if (stream == null)
            throw new ApplicationException("Could not find the questionnaire file.");

        return stream;
    }
}
