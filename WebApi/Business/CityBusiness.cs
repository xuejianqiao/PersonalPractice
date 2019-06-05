using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Model;
using WebApi.Repository;

namespace WebApi.Business
{
    public class CityBusiness: ICityBusiness
    {
        private ICityRepository _iCityRepository;
        public CityBusiness(ICityRepository iCityRepository)
        {
            _iCityRepository = iCityRepository;
        }

       public  bool CreateUserEntity(City entity)
        {
            return _iCityRepository.CreateEntity(entity);
        }
    }
}
