using EmployeeManagmentUI.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagmentUI;

public class LoginContext : IdentityDbContext<EmployeeManagmentUIUser>
{
    public LoginContext(DbContextOptions<LoginContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        builder.ApplyConfiguration(new ApplicationUserEntityConfigrution());


    }
}

public class ApplicationUserEntityConfigrution : IEntityTypeConfiguration<EmployeeManagmentUIUser>
{
    public void Configure(EntityTypeBuilder<EmployeeManagmentUIUser> builder)
    {
        builder.Property(x => x.firstName).HasMaxLength(100);
        builder.Property(x => x.lastName).HasMaxLength(100);
    }
}