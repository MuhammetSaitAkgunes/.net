using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    //applicationdaki repositorynin implementasyonu persistence içinde. klasik mimariden en büyük fark.
    public class BrandRepository : EfRepositoryBase<Brand, BaseDbContext>, IBrandRepository
    {
        public BrandRepository(BaseDbContext context) : base(context) // contexti base'e geç. base efrepo'dan geliyor. oradaki T type'a uygun olarak baseden geliyor.
        {
        }
        
    }
}