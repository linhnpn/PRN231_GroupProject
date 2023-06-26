using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;

public class FirebaseStorageService : IFirebaseStorageService
{
    private readonly StorageClient _storageClient;
    private const string BucketName = "hrm-resource.appspot.com";
    private string baseUrl = "https://firebasestorage.googleapis.com/v0/b/hrm-resource.appspot.com/o/{0}?alt=media";

    public FirebaseStorageService(StorageClient storageClient)
    {
        _storageClient = storageClient;
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        var randomGuid = Guid.NewGuid();

        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);

        await _storageClient.UploadObjectAsync(BucketName, 
            $"{randomGuid}", file.ContentType, stream);
        

        return string.Format(baseUrl, randomGuid.ToString());
    }
}