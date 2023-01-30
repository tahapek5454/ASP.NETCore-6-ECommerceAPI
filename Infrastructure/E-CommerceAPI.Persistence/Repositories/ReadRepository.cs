﻿using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Domain.Entities.Common;
using E_CommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T>
        where T : BaseEntity
    {
        private readonly ECommerceAPIDbContext _context;
        public ReadRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }

        // direkt ilgili context i entity e gore donderecek metod context.set
        // bize diekt ilgili table'ın contex'ini dondereceginden gecmiste yapigimiz zahmetli newlemelerden kurtuluyoruz
        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll()
            => Table;
       
        public async Task<T> GetByIdAsync(string id)
            => await Table.FirstOrDefaultAsync(t => t.Id == Guid.Parse(id));

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
            => await Table.FirstOrDefaultAsync(method);

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
            => Table.Where(method);
        
    }
}
