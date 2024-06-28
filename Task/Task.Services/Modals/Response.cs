
namespace Task.Services.Modals
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public T? Res { get; set; }
        public List<string>? Errors { get; set; }
    }
}
