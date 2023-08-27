using Common.Resource.Interfaces;
using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Services;
using FluentValidation;

namespace ECommerce.Validation.EntitiesValidation;

public class ManagerUserValidator : AbstractValidator<ManagerUser>
{
  private readonly ManagerUserService _managerUserService;

  public ManagerUserValidator(IResourceService eCommerceResourceService, ManagerUserService managerUserService)
  {
    _managerUserService = managerUserService;

    var nameDisplayName = eCommerceResourceService.GetLocalizedText("Name");
    var surnameDisplayName = eCommerceResourceService.GetLocalizedText("Surname");
    var eMailDisplayName = eCommerceResourceService.GetLocalizedText("EMail");
    var passwordDisplayName = eCommerceResourceService.GetLocalizedText("Password");

    RuleFor(m => m.Name)
      .NotNull().WithMessage(eCommerceResourceService.GetLocalizedText("General_Required", nameDisplayName))
      .NotEmpty().WithMessage(eCommerceResourceService.GetLocalizedText("General_Required", nameDisplayName))
      .MaximumLength(100)
      .WithMessage(eCommerceResourceService.GetLocalizedText("General_Max_Length", nameDisplayName, 100))
      .MinimumLength(2)
      .WithMessage(eCommerceResourceService.GetLocalizedText("General_Min_Length", nameDisplayName, 2));

    RuleFor(m => m.Surname)
      .NotNull().WithMessage(eCommerceResourceService.GetLocalizedText("General_Required", surnameDisplayName))
      .NotEmpty().WithMessage(eCommerceResourceService.GetLocalizedText("General_Required", surnameDisplayName))
      .MaximumLength(100)
      .WithMessage(eCommerceResourceService.GetLocalizedText("General_Max_Length", surnameDisplayName, 100))
      .MinimumLength(2)
      .WithMessage(eCommerceResourceService.GetLocalizedText("General_Min_Length", surnameDisplayName, 2));

    RuleFor(m => m.EMail)
      .NotNull().WithMessage(eCommerceResourceService.GetLocalizedText("General_Required", eMailDisplayName))
      .NotEmpty().WithMessage(eCommerceResourceService.GetLocalizedText("General_Required", eMailDisplayName))
      .MaximumLength(100)
      .WithMessage(eCommerceResourceService.GetLocalizedText("General_Max_Length", eMailDisplayName, 100))
      .MinimumLength(3)
      .WithMessage(eCommerceResourceService.GetLocalizedText("General_Min_Length", eMailDisplayName, 3))
      .EmailAddress().WithMessage(eCommerceResourceService.GetLocalizedText("EMail_Invalid"))
      .Must(BeUniqueEmail).WithMessage("Mail Adresi Daha Önce Tanımlanmış");

    RuleFor(m => m.Password)
      .NotNull().WithMessage(eCommerceResourceService.GetLocalizedText("General_Required", passwordDisplayName))
      .NotEmpty().WithMessage(eCommerceResourceService.GetLocalizedText("General_Required", passwordDisplayName))
      .MaximumLength(20)
      .WithMessage(eCommerceResourceService.GetLocalizedText("General_Max_Length", passwordDisplayName, 20))
      .MinimumLength(5)
      .WithMessage(eCommerceResourceService.GetLocalizedText("General_Min_Length", passwordDisplayName, 5));
  }

  private bool BeUniqueEmail(string email)
  {
    return _managerUserService.IsMailAdressUniq(email);
  }

}