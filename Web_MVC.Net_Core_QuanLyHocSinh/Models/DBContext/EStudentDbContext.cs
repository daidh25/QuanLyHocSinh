using Microsoft.EntityFrameworkCore;
using Web_MVC.Net_Core_QuanLyHocSinh.Models.DTO;

namespace Web_MVC.Net_Core_QuanLyHocSinh.Models.DBContext
{
    public class EStudentDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-GS7QPAK3\\SQLEXPRESS;Initial Catalog=QuanLyHocSinh");
        }
        public virtual DbSet<Student> Students { get; set; }
    }
}
