using E_CommerceAPI.Application.Repositories.OwnFileRepository.ProductImageFileRepostitory;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.ProductImageFiles.ChangeShowCase
{
    public class ChangeShowCaseCommandHandler : IRequestHandler<ChangeShowCaseCommandRequest, ChangeShowCaseCommandResponse>
    {
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        public ChangeShowCaseCommandHandler(IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<ChangeShowCaseCommandResponse> Handle(ChangeShowCaseCommandRequest request, CancellationToken cancellationToken)
        {
            var query = _productImageFileWriteRepository.Table.Include(p => p.Products).SelectMany(p => p.Products, (pif, p) => new
            {
                pif,
                p
            });

            var data = await query.FirstOrDefaultAsync(p => p.p.Id == Guid.Parse(request.ProductId) && p.pif.Showcase);

            if(data != null)
            {
                data.pif.Showcase = false;
            }

            var image = await query.FirstOrDefaultAsync(p => p.pif.Id == Guid.Parse(request.ImageId));
            
            if(image != null)
            {
                image.pif.Showcase = true;
            }

            await _productImageFileWriteRepository.SaveAsync();

            return new();
        }
    }
}
