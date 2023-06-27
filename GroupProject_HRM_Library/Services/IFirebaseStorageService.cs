using Microsoft.AspNetCore.Http;

public interface IFirebaseStorageService
{
    public Task<string> UploadFile(IFormFile file);
}