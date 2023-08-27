using AutoMapper;
using Common.Core.Classes;
using Common.Core.Enums;
using Common.Core.Repository;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Context;

namespace ECommerce.Infrastructure.Services;

public class ProductService
{
  private readonly IBaseRepository<Product, ECommerceDbContext> _producRepository;
  private readonly IBaseRepository<ProductImages, ECommerceDbContext> _producImagesRepository;
  private readonly IMapper _mapper;

  public ProductService(IUnitOfWork<ECommerceDbContext> unitOfWork, IMapper mapper)
  {
    _producRepository = unitOfWork.Repository<Product>();
    _producImagesRepository = unitOfWork.Repository<ProductImages>();
    _mapper = mapper;
  }

  public async Task<BaseResponse<Product>> GetListAsync()
  {
    return await _producRepository.GetProtectedListWithIncludeAsync(t => !t.Deleted, t => t.Category);
  }

  public async Task<BaseResponse<Product>> GetAsync(string key)
  {
    return await _producRepository.GetByEnchKeyWithIncludeAsync(key, t => t.ProductImages);
  }

  public async Task<BaseResponse<Product>> AddAsync(Product model)
  {
    var result = await _producRepository.AddAsync(model);

    if (result.ResultStatus == ResultStatus.Success)
      result.Url = "/Product/List";

    return result;
  }

  public async Task<BaseResponse<Product>> UpdateAsync(Product model)
  {
    var categoryResponse = await _producRepository.GetByEnchKeyAsync(model.EnchKey);

    categoryResponse.Data = _mapper.Map<Product>(model);

    var result = await _producRepository.UpdateAsync(categoryResponse.Data);

    if (result.ResultStatus == ResultStatus.Success)
      result.Url = "/Product/List";

    return result;
  }


  public async Task<BaseResponse<ProductImages>> ProductImageAdd(ProductImages model)
  {
    return await _producImagesRepository.AddAsync(model);
  }

  public async Task<BaseResponse<ProductImages>> GetProductImagesByProductId(int productId)
  {
    return await _producImagesRepository.GetListAsync(t => t.ProductId == productId && t.Active && !t.Deleted);
  }

  public async Task<BaseResponse<ProductImages>> GetProductImage(int id)
  {
    return await _producImagesRepository.GetAsync(t => t.Id == id);
  }

  public async Task<BaseResponse<ProductImages>> DeleteProductImage(ProductImages model)
  {
    return await _producImagesRepository.DeleteAsync(model);
  }
}