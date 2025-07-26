namespace LapShop_Project.Models
{
    public class ApiResponse
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public int StatusCode { get; set; }
    }
}
