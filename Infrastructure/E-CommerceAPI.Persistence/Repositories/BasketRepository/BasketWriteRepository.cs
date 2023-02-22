using E_CommerceAPI.Application.Repositories.BasketRepository;
using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Repositories.BasketRepository
{
    public class BasketWriteRepository : WriteRepository<Basket>, IBasketWriteRepository
    {
        public BasketWriteRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
