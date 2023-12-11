namespace PublicTransportApi.Services;

public class Result<T> : Result
{
    public T Data { get; set; }
}