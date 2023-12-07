namespace API.ResponseModels;

public class ResponseModel<T>
{
    public ResponseModel(bool success = false)
    {
        Success = success;

        if (!success)
        {
            Error = new ResponseErrorModel();
        }
    }

    public bool Success { get; set; }
    public T? Result { get; set; }
    public ResponseErrorModel? Error { get; set; }
}
