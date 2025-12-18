namespace Api
{
    public class ApiResponse
    {
        public object Data { get; set; } = new object();
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
        public int StatusCode { get; set; }
    }
}
