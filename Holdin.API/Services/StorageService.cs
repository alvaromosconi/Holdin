namespace Holdin.API.Services;

public class StorageService
{
    private static readonly string _rootPath = Environment.GetEnvironmentVariable("STORAGE");

    public StorageService()
    {
    }

    public string[] GetContent(string relativePath)
    {
        string? relativePathNormalized = relativePath.Replace("-", "/");
        string? joinedPath = Path.Combine(_rootPath, relativePathNormalized);
        string[] files = Directory.GetFileSystemEntries(joinedPath);

        string[] fileNames = files.Select(f => Path.GetFileName(f)).ToArray(); 

        return files;
    }

    public async Task Upload(string relativePath, IFormFile[] files)
    {
        string? relativePathNormalized = relativePath.Replace("-", "/");
        string? joinedPath = Path.Combine(_rootPath, relativePathNormalized);

        foreach (FormFile file in files)
        {
            var filePath = Path.Combine(joinedPath, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }

    public FileStream GetFile(string relativePath)
    {
        string relativePathNormalized = relativePath.Replace("-", "/");
        string joinedPath = Path.Combine(_rootPath, relativePathNormalized);

        FileStream file = File.Open(joinedPath, FileMode.Open, FileAccess.Read);

        return file;
    }

}
