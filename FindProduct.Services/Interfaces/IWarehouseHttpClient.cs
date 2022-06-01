using System.Threading.Tasks;

namespace FindProduct.Services.Interfaces
{
    public interface IWarehouseHttpClient<T>
    {
        Task<T> GetAsync(string url,string parameter);
    }
}
