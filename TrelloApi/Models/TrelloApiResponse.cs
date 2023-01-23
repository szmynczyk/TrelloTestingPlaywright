namespace TrelloApi.Models
{
    public class TrelloApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Url { get; set; }
        public T? Data { get; set; }
    }
}
