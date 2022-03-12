namespace Broooms.Catalog.Providers;

using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

public class AzureBlobFileManager : IFileManager
{
    private readonly CloudStorageAccount _account;
    private readonly CloudBlobContainer _container;

    public AzureBlobFileManager(string connectionString, string container)
    {
        _account = CloudStorageAccount.Parse(connectionString);
        _container = GetOrCreateContainerAsync(container).Result;
        _container.BeginSetPermissions(
            new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob },
            (a) => { },
            null
        );
    }

    private async Task<CloudBlobContainer> GetOrCreateContainerAsync(string container)
    {
        var blobClient = _account.CreateCloudBlobClient();
        var blobContainer = blobClient.GetContainerReference(container);
        await blobContainer.CreateIfNotExistsAsync();
        return blobContainer;
    }

    public async Task<Uri> UploadFileAsync(string blobName, byte[] content, string contentType)
    {
        var blockBlob = _container.GetBlockBlobReference(blobName);
        blockBlob.Properties.ContentType = contentType;
        await blockBlob.UploadFromByteArrayAsync(content, 0, content.Length);
        return blockBlob.Uri;
    }

    public Task DeleteFileAsync(string filePath)
    {
        var blob = _container.GetBlockBlobReference(filePath);
        return blob.DeleteIfExistsAsync();
    }
}
