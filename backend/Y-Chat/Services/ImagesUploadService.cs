using System.Drawing;
using System.Text.Json;
using System.Text.RegularExpressions;
using Y_Chat.Responses;
using FormUrlEncodedContent = System.Net.Http.FormUrlEncodedContent;

namespace Y_Chat.Services;

public class ImagesUploadService(HttpClient httpClient, IConfiguration configuration, ILogger<ImagesUploadService> logger) : IImagesUploadService
{
    public async Task<string> UploadImage(string image)
    {
        var imgbbKey = configuration["AppKeys:ImgBB"];
        var verifyUrl = $"https://api.imgbb.com/1/upload?key={imgbbKey}";
        
        if (string.IsNullOrEmpty(image))
            throw new Exception("Image is empty");

        var base64Data = Regex.Replace(image, @"^data:image\/[a-z]+;base64,", "");
        logger.LogInformation(base64Data);
        
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("image", base64Data),
        });
        
        var response = await httpClient.PostAsync(verifyUrl, content);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        
        jsonResponse = jsonResponse.Replace("\\", "");
        logger.LogInformation(jsonResponse);
        ImgBBResponse? imgBBResponse = null;
        try
        {
            imgBBResponse = JsonSerializer.Deserialize<ImgBBResponse>(jsonResponse);
        }
        catch (Exception ex)
        {
            logger.LogInformation(ex.Message);
            throw new Exception(ex.Message);
        }

        if (imgBBResponse is null)
        {
            throw new Exception("Image is empty");
        }
        if (!imgBBResponse.Success)
        {
            throw new Exception("Image upload failed");
        }

        return imgBBResponse.Data.Image.Url;
    }
}