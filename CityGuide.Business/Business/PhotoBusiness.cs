using CityGuide.Domain.Entity;
using CityGuide.Domain.Interface;
using CityGuide.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CityGuide.Business.Business
{
    public class PhotoBusiness : Repository<Photo>, IPhotoBusiness
    {
        public PhotoBusiness(CityGuideDataContext context) : base(context)
        {
        }
    }
}
