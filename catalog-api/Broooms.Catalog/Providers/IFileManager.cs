namespace Broooms.Catalog.Providers;

public interface IFileManager
{
    Task<Uri> UploadFileAsync(string blobName, byte[] content, string contentType);
    Task DeleteFileAsync(string filePath);
}
