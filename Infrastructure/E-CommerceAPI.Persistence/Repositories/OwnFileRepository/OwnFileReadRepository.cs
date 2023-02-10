using E_CommerceAPI.Application.Repositories.OwnFileRepository;
using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Persistence.Repositories.OwnFileRepository
{
    public class OwnFileReadRepository : ReadRepository<OwnFile>, IOwnFileReadRepository
    {
        public OwnFileReadRepository(ECommerceAPIDbContext context) : base(context)
        {
        }
    }
}
