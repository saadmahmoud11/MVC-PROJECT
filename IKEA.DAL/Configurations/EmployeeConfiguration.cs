using IKEA.DAL.Models.Employees;
using IKEA.DAL.Models.Shared;

namespace IKEA.DAL.Configurations;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(e => e.Name)
            .HasColumnType("varchar(50)");
        builder.Property(e => e.Address)
            .HasColumnType("varchar(150)");
        builder.Property(e => e.Salary)
            .HasColumnType("decimal(18,2)");
        builder.Property(e => e.Gender)
            .HasConversion((empGender) => empGender.ToString(), (gender) => (Gender)Enum.Parse(typeof(Gender), gender));
        builder.Property(e => e.EmployeeType)
    .HasConversion((empType) => empType.ToString(), (type) => (EmployeeType)Enum.Parse(typeof(EmployeeType), type));

    }
}
