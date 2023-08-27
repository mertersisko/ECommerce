using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Helper.Encrypt;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Persistence.Seed
{
  public class ManagerUserConfigration : IEntityTypeConfiguration<ManagerUser>
  {
    public void Configure(EntityTypeBuilder<ManagerUser> builder)
    {
      builder.HasData(new ManagerUser
      {
        Password = Md5Encrypt.Encrypt("12345"),
        EMail = "kagan@kagan.com",
        Active = true,
        CreatedBy = "Configrator",
        CreatedDate = DateTime.Now,
        Deleted = false,
        Id = 1,
        LastModifiedBy = "Configrator",
        LastModifiedDate = DateTime.Now,
        Name = "Kağan",
        Surname = "AZAR"
      });
    }
  }
}
