namespace BisHelpers.Domain.Models;
public class Response
{
    public bool IsSuccess { get; set; }

    public ErrorBody ErrorBody { get; set; } = new();
}

public class Response<T> where T : class
{
    public bool IsSuccess { get; set; }

    public T? Model { get; set; }

    public ErrorBody ErrorBody { get; set; } = new();
}
