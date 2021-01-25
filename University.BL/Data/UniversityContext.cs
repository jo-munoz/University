using System.Data.Entity;
using University.BL.Models;

namespace University.BL.Data
{
    public class UniversityContext : DbContext
    {
        private static UniversityContext universityContext = null;

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
            //if (universityContext == null)
            //{
            //    universityContext = new UniversityContext();
            //}
            return new UniversityContext(); ;
        }
    }
}
