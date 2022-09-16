using System.Globalization;

namespace microservice.domain.Exceptions;

public class ConectionException : Exception
{
    public ConectionException() : base()
    {
    }

    public ConectionException(string message) : base(message)
    {
    }

    public ConectionException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }

    public ConectionException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
