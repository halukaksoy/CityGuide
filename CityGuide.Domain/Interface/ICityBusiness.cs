using System;
using System.Collections.Generic;
using System.Text;
using CityGuide.Domain.Entity;

namespace CityGuide.Domain.Interface
{
    public interface ICityBusiness : IRepository<City>
    {
        List<Photo> GetPhotosByCity(int cityId);
    }
}
