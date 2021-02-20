using Microsoft.EntityFrameworkCore;
using PaymentMerchant.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PaymentMerchant.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> :  IRepository<T> where T : class
    {
        protected readonly DataContext _dataContext;
        private readonly DbSet<T>  entitySet;
    
        public BaseRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            entitySet = dataContext.Set<T>();
      
        }


        public async Task Delete(int? id)
        {
            if (await Exist(id))
            {
                var entity = await Find(id);

                _dataContext.Remove(entity);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<bool> Exist(int? id)
        {
            var entity = await entitySet.FindAsync(id);
            return   entity != null ? true : false;
        }

        public async Task<T> Find(int? id)
        {
            var entity = await  entitySet.FindAsync(id);
            return entity;
        }

        public async Task<T> Top()
        {
            var entity = await entitySet.FirstOrDefaultAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> FindAll()
        {
            return await entitySet.ToListAsync();
        }

 
        public async Task Save(T t)
        {
            if (t != null)
            {
                entitySet.Add(t);
                await _dataContext.SaveChangesAsync();
              
              
            }

        }

        public async Task Update(T t)
        {     
            if (t !=null )
            {
                _dataContext.Update(t);
                await _dataContext.SaveChangesAsync();
            }
        }

        public async Task<int> Count()
        {
            return await entitySet.CountAsync();
        }



        public async Task AddRange(T[] t)
        {
            _dataContext.AddRange(t);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateRange(T[] t)
        {
            if (t != null)
            {
                _dataContext.UpdateRange(t);
                await _dataContext.SaveChangesAsync();
            }
        }


    

    
    }
}
