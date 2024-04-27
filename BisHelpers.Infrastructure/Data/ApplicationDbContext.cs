namespace BisHelpers.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Student> Students { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.NoAction;

        #region ConfigureKeys
        builder.Entity<RefreshToken>()
            .HasKey(e => new { e.Id, e.UserId });
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

        builder.Entity<AppUser>()
            .HasOne(u => u.Student)
            .WithOne(u => u.User)
            .HasForeignKey<Student>(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Student>()
            .HasOne(u => u.LastUpdatedBy)
            .WithMany()
            .HasForeignKey(u => u.LastUpdatedById)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Student>()
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
