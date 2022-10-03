namespace Application.Data
{
    public class CustomException : Exception
    {
        public CustomException(string message, string details)
        {
            Message = message;
            Details = details;
        }
      
        public string Message { get; set; }

        public string Details { get; set; }
    }
}
