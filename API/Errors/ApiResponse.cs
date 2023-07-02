namespace API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Title { get; set; }

        public ApiResponse(int statusCode, string title = null)
        {
            StatusCode = statusCode;
            Title = title ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "You've Sent A Bad Request",
                401 => "Authorization Required",
                404 => "Page Not Found",
                500 => "Internal Server Error",
                _=> null
            };
        }
    }
}
