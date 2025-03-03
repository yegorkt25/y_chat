using System.Drawing;

namespace Y_Chat.Services;

public class ImageValidationService
{
    public static async Task<string?> ValidateImage(string imageUrl)
    {
        using var client = new HttpClient();
        byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);
        
        using var ms = new MemoryStream(imageBytes);
        
        var bitmap = new Bitmap(ms);
        if (bitmap.Width != bitmap.Height)
        {
            return null;
        }
        
        return imageUrl;
    }
}