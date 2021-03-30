namespace Quotes.Common.Wrappers
{
    public class GenericResponse<T>  : Response
    {
        public T Data { get; set; }
    }
}