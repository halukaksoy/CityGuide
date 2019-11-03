using System;
using System.Collections.Generic;
using System.Text;
using CityGuide.Domain.Interface;
using CityGuide.Infrastructure.Data;
using CityGuide.Domain.Entity;
using System.Linq;

namespace CityGuide.Business.Business
{
    public class CityBusiness : Repository<City>, ICityBusiness
    {
        private CityGuideDataContext _context;
        public CityBusiness(CityGuideDataContext context) : base(context)
        {
            _context = context;
        }

        public List<Photo> GetPhotosByCity(int cityId)
        {
            return _context.Photos.Where(x => x.CityId == cityId).ToList();
        }
    }
}
