using E_CommerceAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Repositories
{
    public interface IReadRepository<T>:IRepository<T>
        where T : BaseEntity 
    {
        // List deseydik inmemory icerirdi, Sorgu uzerinde calismak için IQueryavle kullanacgiz
        IQueryable<T> GetAll();
        IQueryable<T> GetWhere(Expression<Func<T, bool>> method);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method);
        Task<T> GetByIdAsync(string id);

    }
   
}
