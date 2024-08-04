using Microsoft.EntityFrameworkCore;

namespace CrudWithImageUploading.Models
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentInfo> StudentsInfo { get; set;}
    }
}
