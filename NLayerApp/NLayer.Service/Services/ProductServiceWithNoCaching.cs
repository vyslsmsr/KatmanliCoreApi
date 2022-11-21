using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ProductServiceWithNoCaching : Service<Product>, IProductService
    {
        // product repository ile bağlantı kurduk
        private readonly IProductRepository _productRepository;
        // mapleeme işlemleri için bağlantı kurduk
        private readonly IMapper _mapper;

        public ProductServiceWithNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }


        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWitCategory()
        {
            // Get category methodunu çağırdık.
            var product = await _productRepository.GetProductsWitCategory();
            // buradan ise gelen product mapleyip productdto objesi içine attık
            var pdocutsDto= _mapper.Map<List<ProductWithCategoryDto>>(product);
            // bu kısımda ise productdto geriye success ile dönüyoruz.
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, pdocutsDto);
        }
    }
}
