using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoAnPM_TH_.Models
{

    public class ApplicationIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationIdentityDbContext(DbContextOptions<ApplicationIdentityDbContext> options)
            : base(options)
        {
        }

        // Thêm các DbSet khác của bạn nếu cần, nhưng Identity sẽ tự động thêm các bảng cần thiết
    }
}
