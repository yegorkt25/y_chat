namespace Y_Chat.Services;

public interface IImagesUploadService
{
    public Task<string> UploadImage(string image);
    
}