using AutoMapper;
using Common.Core.Classes;

namespace Common.Core.AutoMapperModule;

public class IgnorePropertiesProfile<TSource, TDestination> : Profile where TDestination : BaseEntity
{
  public IgnorePropertiesProfile()
  {
    CreateMap<TSource, TDestination>()

      .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
      .ForMember(dest => dest.LastModifiedDate, opt => opt.Ignore())
      .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
      .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
      .ForMember(dest => dest.Active, opt => opt.Ignore())
      .ForMember(dest => dest.Deleted, opt => opt.Ignore())
      .ForMember(dest => dest.EnchKey, opt => opt.Ignore());
  }
}