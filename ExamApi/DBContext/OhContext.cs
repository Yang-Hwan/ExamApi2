using Microsoft.EntityFrameworkCore;
using ExamModel;

namespace ExamApi.DBContext
{
    public class OhContext : DbContext
    {

        public OhContext(DbContextOptions<OhContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MenuAuth>()
                .HasKey(nameof(ExamModel.MenuAuth.Auth));
            modelBuilder.Entity<MemInfo>()
                .HasKey(nameof(ExamModel.MemInfo.Auth));

        }

        public DbSet<ExamModel.MemInfo> MemInfo { get; set; }
        public DbSet<ExamModel.MemWallet> MemWallet { get; set; }
        public DbSet<ExamModel.MemWalletLog> MemWalletLog { get; set; }
        public DbSet<ExamModel.Auth> Auth { get; set; }
        public DbSet<ExamModel.Menu> Menu { get; set; }
        public DbSet<ExamModel.MenuAuth> MenuAuth { get; set; }

    }
}
