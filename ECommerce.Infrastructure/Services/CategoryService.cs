using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Common.Core.Classes;
using Common.Core.Enums;
using Common.Core.Repository;
using ECommerce.Domain.Entities;
using ECommerce.Persistence.Context;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Infrastructure.Services
{
  public class CategoryService
  {
    private readonly IBaseRepository<Category, ECommerceDbContext> _categoryBaseRepository;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork<ECommerceDbContext> unitOfWork, IMapper mapper)
    {
      _categoryBaseRepository = unitOfWork.Repository<Category>();
      _mapper = mapper;
    }

    public async Task<BaseResponse<Category>> GetListAsync()
    {
      return await _categoryBaseRepository.GetProtectedListAsync(t => !t.Deleted);
    }

    public async Task<BaseResponse<Category>> GetListAsyncV1()
    {
      return await _categoryBaseRepository.GetListAsync(t => !t.Deleted);
    }

    public async Task<BaseResponse<Category>> GetById(int id)
    {
      return await _categoryBaseRepository.GetAsync(t => t.Id == id);
    }

    public async Task<BaseResponse<Category>> GetAsync(string id)
    {
      return await _categoryBaseRepository.GetByEnchKeyAsync(id);
    }

    public async Task<BaseResponse<Category>> AddAsync(Category model)
    {
      if (model.ParentCategoryId == 0)
        model.ParentCategoryId = null;

      var result = await _categoryBaseRepository.AddAsync(model);

      if (result.ResultStatus == ResultStatus.Success)
        result.Url = "/Category/List";

      return result;
    }

    public async Task<BaseResponse<Category>> UpdateAsync(Category model)
    {
      var result = await _categoryBaseRepository.UpdateAsync(model);

      if (result.ResultStatus == ResultStatus.Success)
        result.Url = "/Category/List";

      return result;
    }

    public async Task<SelectList> GetCategoriesSelectList()
    {
      var selectList = new Dictionary<int, string>();

      var categoryList = await _categoryBaseRepository.GetListWithIncludeAsync(t => !t.Deleted && t.Active, t => t.ParentCategory);

      var data = string.Empty;

      foreach (var category in categoryList.DataList)
      {
        if (category.ParentCategoryId == null)
        {
          data = category.Definition;
        }
        else
        {
          int? id = category.Id;

          while (id != null)
          {
            foreach (var item in categoryList.DataList)
            {
              if (item.Id == id)
              {
                if (item.ParentCategoryId != null)
                  data = " > " + item.Definition + data;
                else
                  data = item.Definition + data;

                id = item.ParentCategoryId;
                break;
              }
            }
          }
        }

        selectList.Add(category.Id, data);
        data = string.Empty;
      }

      return new SelectList(selectList.OrderBy(t => t.Value), "Key", "Value");
    }
  }
}
