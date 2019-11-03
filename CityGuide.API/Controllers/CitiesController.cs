using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CityGuide.Domain.Interface;
using CityGuide.Domain.Dtos;
using CityGuide.Domain.Entity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace CityGuide.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private ICityBusiness _cityBusiness;
        private IMapper _mapper;

        public CitiesController(ICityBusiness cityBusiness, IMapper mapper)
        {
            _cityBusiness = cityBusiness;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult Get()
        {
            var result = _cityBusiness.GetAll(x => 1 == 1, x => x.Photos, x => x.User).ToList();
            var mappedResult = _mapper.Map<List<CityListDto>>(result);
            return Ok(mappedResult);
        }

        [HttpGet]
        [Route("detail/{id}")]
        public ActionResult Get(int id)
        {
            var result = _cityBusiness.GetAll(x => x.Id == id, x => x.Photos).FirstOrDefault();
            var mappedResult = _mapper.Map<CityDetailDto>(result);
            return Ok(mappedResult);
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add([FromBody]City city)
        {
            _cityBusiness.Add(city);
            _cityBusiness.SaveAll();
            return Ok(city);
        }

        [HttpPost]
        [Route("delete")]
        public ActionResult Delete([FromBody]City city)
        {
            _cityBusiness.Delete(city);
            _cityBusiness.SaveAll();
            return Ok(new { IsSuccess = true });
        }

        [HttpGet]
        [Route("photos/{cityId}")]
        public ActionResult GetPhotosByCity(int cityId)
        {
            var result = _cityBusiness.GetPhotosByCity(cityId);
            return Ok(result);
        }
    }
}