using E_CommerceAPI.Application.Abstractions.Storage;
using E_CommerceAPI.Application.Repositories.OwnFileRepository;
using E_CommerceAPI.Application.Repositories.OwnFileRepository.InvoiceFileRepository;
using E_CommerceAPI.Application.Repositories.OwnFileRepository.ProductImageFileRepostitory;
using E_CommerceAPI.Application.Repositories.ProductRepository;
using E_CommerceAPI.Application.RequestParameters;
using E_CommerceAPI.Application.Services;
using E_CommerceAPI.Application.ViewModels.Products;
using E_CommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IFileServiceAlternative _fileService;

        private readonly IOwnFileReadRepository _ownFileReadRepository;
        private readonly IOwnFileWriteRepository _ownFileWriteRepository;

        private readonly IProductImageFileReadRepository _productImageFileReadRepository;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;

        private readonly IStorageService _storageService;

        // wwwroot un pathine ulasmak için -> statik filelara erisim saglar
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProductsController(
            IProductReadRepository productReadRepository,
            IProductWriteRepository productWriteRepository,
            IWebHostEnvironment webHostEnvironment,
            IFileServiceAlternative fileService,
            IInvoiceFileWriteRepository invoiceFileWriteRepository,
            IInvoiceFileReadRepository ınvoiceFileReadRepository,
            IProductImageFileWriteRepository productImageFileWriteRepository,
            IProductImageFileReadRepository productImageFileReadRepository,
            IOwnFileReadRepository ownFileReadRepository,
            IOwnFileWriteRepository ownFileWriteRepository,
            IStorageService storageService
            )
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _invoiceFileReadRepository = ınvoiceFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _ownFileReadRepository = ownFileReadRepository;
            _ownFileWriteRepository = ownFileWriteRepository;
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]Pagination pagination)
        {
            int totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreateDate,
                p.UpdateDate
            }).Skip(pagination.Size * pagination.Page).Take(pagination.Size).ToList();

            return Ok(new
            {
                totalCount,
                products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_CreateProduct model)
        {
            _ = await _productWriteRepository.AddAsync(new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });

            _ = await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(string id)
        {

            // var datas = await _storageService.UploadAsync("resource\\ownFile", Request.Form.Files);

            var product = await _productReadRepository.GetByIdAsync(id);

            var datas = await _storageService.UploadAsync("products", Request.Form.Files);

            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product}
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();

            // var datas = _storageService.HasFile("deneme", "taha-pek-4.jpg");

            // var data = _storageService.GetFiles("invoices");

            // await _storageService.DeleteAsync("invoices", "as-2.png");


            //var datas = await _fileService.UploadAsync("resource\\ownFile", Request.Form.Files);

            ////await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile
            ////{
            ////    FileName = d.fileName,
            ////    Path = d.path,
            ////}).ToList());

            ////await _productImageFileWriteRepository.SaveAsync();


            ////await _invoiceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoiceFile
            ////{
            ////    FileName = d.fileName,
            ////    Path = d.path,
            ////    Price = new Random().Next()
            ////}).ToList());

            ////await _invoiceFileWriteRepository.SaveAsync();

            //await _ownFileWriteRepository.AddRangeAsync(datas.Select(d => new OwnFile
            //{
            //    FileName = d.fileName,
            //    Path = d.path,
            //}).ToList());

            //await _ownFileWriteRepository.SaveAsync();


            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(VM_UpdateProduct model)
        {
            Product updatedProduct = await _productReadRepository.GetByIdAsync(model.Id);
            updatedProduct.Name = model.Name;
            updatedProduct.Price = model.Price;
            updatedProduct.Stock = model.Stock;
 
            var a = await _productWriteRepository.SaveAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var a = await _productWriteRepository.RemoveAsync(id);
            var b = await _productWriteRepository.SaveAsync();
            return Ok();
        }


    }
}
