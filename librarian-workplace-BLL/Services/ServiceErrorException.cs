namespace librarian_workplace_BLL.Services
{
    public class ServiceErrorException : Exception
    {
        public int StatusCode { get; }

        public ServiceErrorException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
