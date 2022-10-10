namespace BookLibraryApi.Models
{
    public class APIResponse
    {
        public bool Response { get; set; }
        public string? ResponseMessage { get; set; }

        public int Status { get; set; }
        public object? Data { get; set; }
    }
}
