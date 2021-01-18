using System.Data.Entity;
using University.BL.Models;

namespace University.BL.Data
{
    public class UniversityContext : DbContext
    {
        public UniversityContext() : base("UniversityContext")
        {

        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        /// <summary>
        /// Patron Singleton
        /// </summary>
        /// <returns></returns>
        public static UniversityContext Create()
        {
            return new UniversityContext();
        }
    }
}
