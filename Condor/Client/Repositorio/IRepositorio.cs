namespace Condor.Client.Repositorio
{
    public interface IRepositorio
    {
        Task<HttpResponseWrapper<T>> Get<T>(string url);
        Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar);
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar);
        Task<HttpResponseWrapper<object>> Delete(string url);
    }
}
