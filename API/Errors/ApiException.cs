using System;

namespace API.Errors;

public class ApiException(int statusCode, String message, String? details) 
{
    public int StatusCode { get; set; } = statusCode;
    public String Message { get; set; } = message;
    public String? Details { get; set; } = details;

}
