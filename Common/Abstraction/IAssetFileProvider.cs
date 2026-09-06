namespace Common.Abstraction;

public interface IAssetFileProvider
{
    Stream OpenAsset(string path);
}
