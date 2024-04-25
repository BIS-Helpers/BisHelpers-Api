namespace BisHelpers.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.NoAction;

        #region ConfigureKeys

        #endregion

        #region ConfigureRelations
        builder.Entity<AppUser>()
            .HasOne(u => u.LastUpdatedBy)
            .WithMany()
            .HasForeignKey(u => u.LastUpdatedById)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<AppUser>()
            .HasOne(u => u.CreatedBy)
            .WithMany()
            .HasForeignKey(u => u.CreatedById)
            .OnDelete(DeleteBehavior.NoAction);
        #endregion

        #region DataSeeding

        #endregion

        base.OnModelCreating(builder);
    }
}
