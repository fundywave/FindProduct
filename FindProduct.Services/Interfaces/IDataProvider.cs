using System.Collections.Generic;

namespace FindProduct.Services.Interfaces
{
    public interface IDataProvider<T>
    {
        IEnumerable<T> Get();
    }
}
