using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentMerchant.Infrastructure.Repositories{ 

    public interface IRepository<T>
    {
        Task<IEnumerable<T>> FindAll();
        Task<T> Find(int? id);
        Task Update(T t);
        Task Save(T t);
        Task Delete(int? id);
        Task<bool> Exist(int? id);
        Task AddRange(T[] t);
        Task<int> Count();
        Task<T> Top();
        Task UpdateRange(T[] t);


    }
}
