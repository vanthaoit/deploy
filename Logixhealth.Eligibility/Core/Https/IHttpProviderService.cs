using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogixHealth.Eligibility.Core.Https
{
    public interface IHttpProviderService<T> where T : class
    {
        Task<IEnumerable<T>> GetAsync(string Uri);

        Task<T> GetAsync(string Uri,int id);

        Task<IEnumerable<T>> GetIEnumerableByValueAsync(string Uri);

        Task<T> PostAsync(string Uri, T data);

        Task<T> PutAsync(string Uri,T data);

        Task<T> DeleteSingleAsync(string Uri, T data);

        Task<bool> DeleteMultiAsync(string Uri, IEnumerable<T> data);
    }
}
