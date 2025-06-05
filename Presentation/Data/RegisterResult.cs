namespace Presentation.Data;

public class RegisterResult
{
   public bool Success { get; set; } 
    public string? Error { get; set; }
}

public class RegisterResult<T> : RegisterResult
{
    public T? Result { get; set; }
}