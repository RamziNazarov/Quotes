namespace Quotes.Common.Wrappers
{
    public class GenericResponse<T> 
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}