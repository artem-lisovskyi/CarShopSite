using CarShopCourseWork.Db;
using CarShopCourseWork.Model.Data;
using CarShopCourseWork.Model.interfaces;
using Newtonsoft.Json;

namespace CarShopCourseWork.Model.data
{
    public class DataMakeRepository : IMakeRepository
    {
        private List<Make> _makes;
        private readonly CarDbContext _dbContext;

        public DataMakeRepository(CarDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IEnumerable<Make> Make => _dbContext.Makes.ToList();
    }

}

