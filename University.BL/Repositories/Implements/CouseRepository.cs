using University.BL.Data;
using University.BL.Models;

namespace University.BL.Repositories.Implements
{
    public class CouseRepository : GenericRepository<Course>
    {
        public CouseRepository(UniversityContext universityContext) : base(universityContext)
        {

        }
    }
}
