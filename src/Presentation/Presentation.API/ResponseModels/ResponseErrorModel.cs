namespace API.ResponseModels;
public class ResponseErrorModel
{
    public string? Message { get; set; }
    public Dictionary<string, List<string>>? BulkMessages { get; set; } = null;
    public string? Code { get; set; }
}
