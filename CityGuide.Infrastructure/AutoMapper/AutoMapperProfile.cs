using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CityGuide.Domain.Entity;
using CityGuide.Domain.Dtos;
using System.Linq;

namespace CityGuide.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<City, CityListDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(source => source.Photos.Any() ? source.Photos.FirstOrDefault(x => x.IsMain).Url : string.Empty);

                }).ForMember(dest => dest.UserName, opt =>
                {
                    opt.MapFrom(source => source.User.UserName);
                });
            CreateMap<Photo, PhotoDto>();

            CreateMap<City, CityDetailDto>();

            CreateMap<Photo, PhotoSaveDto>();
            CreateMap<PhotoSaveDto, Photo>();
        }
    }
}
