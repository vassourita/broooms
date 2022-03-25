namespace Broooms.Auth.Data;

using Broooms.Auth.Accounts;

using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<UserAccount> Accounts { get; set; }
    public DbSet<AccessToken> AccessTokens { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Claim> Claims { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserAccount>(
            table =>
            {
                table.ToTable("UserAccounts");

                table
                    .HasMany(x => x.AccessTokens)
                    .WithOne(x => x.Account)
                    .HasForeignKey(x => x.UserId);

                table
                    .HasMany(x => x.RefreshTokens)
                    .WithOne(x => x.Account)
                    .HasForeignKey(x => x.UserId);

                table.HasMany(x => x.Claims).WithMany(x => x.Accounts).UsingEntity("UserClaims");
            }
        );

        modelBuilder.Entity<AccessToken>(
            table =>
            {
                table.ToTable("AccessTokens");

                table
                    .HasOne(x => x.Account)
                    .WithMany(x => x.AccessTokens)
                    .HasForeignKey(x => x.UserId);
            }
        );

        modelBuilder.Entity<RefreshToken>(
            table =>
            {
                table.ToTable("RefreshTokens");

                table
                    .HasOne(x => x.Account)
                    .WithMany(x => x.RefreshTokens)
                    .HasForeignKey(x => x.UserId);
            }
        );

        modelBuilder.Entity<Claim>(
            table =>
            {
                table.ToTable("Claims");

                table.HasMany(x => x.Accounts).WithMany(x => x.Claims).UsingEntity("UserClaims");
            }
        );
    }
}
