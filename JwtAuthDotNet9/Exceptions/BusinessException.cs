namespace JwtAuthDotNet9.Exceptions
{
    public class BusinessException : AppException
    {
        public BusinessException(string message)
            : base(message, StatusCodes.Status400BadRequest) { }
    }

    public class NotFoundException : AppException
    {
        public NotFoundException(string message)
            : base(message, StatusCodes.Status404NotFound) { }
    }

    public class UnauthorizedException : AppException
    {
        public UnauthorizedException(string message)
            : base(message, StatusCodes.Status401Unauthorized) { }
    }

}
