using E_CommerceAPI.Application.Abstractions.Storage;
using E_CommerceAPI.Application.Features.Commands.CreateProduct;
using E_CommerceAPI.Application.Features.Queries.GetAllProduct;
using E_CommerceAPI.Application.Repositories.OwnFileRepository;
using E_CommerceAPI.Application.Repositories.OwnFileRepository.InvoiceFileRepository;
using E_CommerceAPI.Application.Repositories.OwnFileRepository.ProductImageFileRepostitory;
using E_CommerceAPI.Application.Repositories.ProductRepository;
using E_CommerceAPI.Application.RequestParameters;
using E_CommerceAPI.Application.Services;
using E_CommerceAPI.Application.ViewModels.Products;
using E_CommerceAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        private readonly IConfiguration _configuration;

        //artık uzun uzun yazmaya son
        // application katmanında serviceRegistrationu tanımlamıstık ordan ilgil bagımlılıklar IOC ye eklenecek zaten
        private readonly IMediator _mediator;


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
            IStorageService storageService,
            IConfiguration configuration,
            IMediator mediator
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
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]GetAllProductQueryRequest getAllProductQueryRequest)
        {
            // biz sende metodu kullanrak ona request nesnesini yolladık
            // artık mediator bizim application yaptıgımız tanımlamara gore IOC den de ilgil bagımlılı alarak handler çalıştırıcak
            GetAllProductQueryResponse response =  await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            _ = await _mediator.Send(createProductCommandRequest);

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

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductsImage(string id)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            return Ok(product.ProductImageFiles.Select(p => new {

                Path = $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                p.FileName,
                p.Id
            
            }));

        }

        [HttpDelete("[action]/{productId}")]
        public async Task<IActionResult> DeleteProductImage(string productId, string imageId)
        {
            // product Listesinden sil
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(productId));
            ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));          
            product.ProductImageFiles.Remove(productImageFile);
            await _productWriteRepository.SaveAsync();

            // buluttan sildik
            var image = await _productImageFileReadRepository.GetByIdAsync(imageId);
            await _storageService.DeleteAsync("products", image.FileName);

            // image tablosundan silelim
            _productImageFileWriteRepository.Remove(image);
            await _productImageFileWriteRepository.SaveAsync();

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
