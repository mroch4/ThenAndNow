namespace ThenAndNow.Models.Database
{
    public class Response<T>
    {
        public T[] Items { get; set; }
        public int Total { get; set; }
    }
}
