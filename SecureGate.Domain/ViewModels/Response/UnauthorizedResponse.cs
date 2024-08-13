namespace SecureGate.Domain.ViewModels.Response
{
    public class UnauthorizedResponse
    {
        public UnauthorizedResponse()
        {
            Message = "You are not authorized!";
        }
        public string Message { get; set; }
    }
}
