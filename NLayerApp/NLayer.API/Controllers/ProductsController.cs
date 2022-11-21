using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Service.Services;

namespace NLayer.API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]

    public class ProductsController : CustomBaseController
    {
        // burada mapping işlemini yaptık
        private readonly IMapper _mapprer;
        /// burada productı çağırdık
        //private readonly IService<Product> _service;

        private readonly IProductService _service;

        /// burada her iki işlemi birleştirdik
        public ProductsController(IMapper mapprer, IProductService productService)
        {
            _mapprer = mapprer;
            //_service = service;
            _service = productService;
        }

        /// Get api/products/GetProductsWitCategory
        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWitCategory()
        {
            /// burada ise custum bir sorgu ile hep productları hemde productlara bağlı kategorileri listeledik. 
            return CreateActionResult(await _service.GetProductsWitCategory());
        }

        /// tüm datayı buradan aldık 
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapprer.Map<List<ProductDto>>(products.ToList());
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        /// id göre veri çağırdık 

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var products = await _service.GetByIdAsync(id);
            var productsDtos = _mapprer.Map<ProductDto>(products);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productsDtos));
        }


        // yeni product ekledik
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var products = await _service.AddAsync(_mapprer.Map<Product>(productDto));
            var productsDtos = _mapprer.Map<ProductDto>(products);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productsDtos));
        }

        // veri güncelledik
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
            await _service.UpdateAsync(_mapprer.Map<Product>(productDto));
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        /// id ile veriş sildik
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(204));
        }
    }
}
