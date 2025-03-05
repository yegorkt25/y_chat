using System.Text.Json.Serialization;

namespace Y_Chat.Responses;

public class ImgBBResponse
{
    [JsonPropertyName("data")]
    public ImageData Data { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("status")]
    public int Status { get; set; }
}

public class ImageData
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("url_viewer")]
    public string UrlViewer { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("display_url")]
    public string DisplayUrl { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }

    [JsonPropertyName("time")]
    public int Time { get; set; }

    [JsonPropertyName("expiration")]
    public int Expiration { get; set; }

    [JsonPropertyName("image")]
    public ImageDetails Image { get; set; }

    [JsonPropertyName("thumb")]
    public ImageDetails Thumb { get; set; }

    [JsonPropertyName("medium")]
    public ImageDetails Medium { get; set; }

    [JsonPropertyName("delete_url")]
    public string DeleteUrl { get; set; }
}

public class ImageDetails
{
    [JsonPropertyName("filename")]
    public string Filename { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("mime")]
    public string Mime { get; set; }

    [JsonPropertyName("extension")]
    public string Extension { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }
}
