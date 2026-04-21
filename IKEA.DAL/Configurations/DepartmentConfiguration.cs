using IKEA.DAL.Models.Departments;


namespace IKEA.DAL.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.Property(d => d.Id).UseIdentityColumn(10,10);
        builder.Property(d => d.Name).HasColumnType("varchar(20)");
        builder.Property(d => d.Code).HasColumnType("varchar(20)");
        builder.Property(d => d.CreatedOn).HasDefaultValueSql("GetDate()");
        builder.Property(d => d.LastModifiedOn).HasComputedColumnSql("GetDate()");
        builder.HasMany(d => d.Employees)
            .WithOne(e => e.Department)
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);


    }
}
