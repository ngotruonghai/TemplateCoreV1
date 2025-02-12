using IAuthenticatedUserService = TemplateCore.Application.Interfaces.IAuthenticatedUserService;

namespace TemplateCore.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        public DbSet<KeyActive> KeyActives { get; set; }
        public DbSet<DanhSachQuyTrinh> DanhSachQuyTrinhs { get; set; }
        public DbSet<DiagramNode> DiagramNodes { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<PhongBan> PhongBans { get; set; }
        public DbSet<DanhMucPhanQuyen> DanhMucPhanQuyens { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<NhanSuTheoDoiQuyTrinh> NhanSuTheoDoiQuyTrinhs { get; set; }
        public DbSet<PhongBanTheoDoiQuyTrinh> PhongBanTheoDoiQuyTrinhs { get; set; }
        //public DbSet<ThongTinNoiDungQuyTrinh> ThongTinNoiDungQuyTrinhs { get; set; } Đang không sử dụng
        public DbSet<PhongBanTiepNhanNode> PhongBanTiepNhanNodes { get; set; }
        public DbSet<NhanSuTiepNhanNode> NhanSuTiepNhanNodes { get; set; }
        public DbSet<NodeSetting> NodeSettings { get; set; }
        public DbSet<ThongTinCauHinh> ThongTinCauHinhs { get; set; }
        public DbSet<ThongTinCauHinhBuoc> ThongTinCauHinhBuocs { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.Now;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        entry.Entity.UserParentId = _authenticatedUser.parentUserId?? null;
                        entry.Entity.Status = true;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.Now;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("tempplate");
            builder.Entity<ApplicationUser>().ToTable(t => t.ExcludeFromMigrations());
            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }

        }
    }
}
