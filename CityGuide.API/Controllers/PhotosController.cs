using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CityGuide.Domain.Common.Settings;
using CityGuide.Domain.Dtos;
using CityGuide.Domain.Entity;
using CityGuide.Domain.Interface;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CityGuide.API.Controllers
{
    [Authorize]
    [Route("api/cities/{cityid}/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private IMapper _mapper;
        private IOptions<CloudinarySettings> _cloudinarySettings;
        private IPhotoBusiness _photoBusiness;
        private ICityBusiness _cityBusiness;
        private Cloudinary _cloudinary;
        public PhotosController(IMapper mapper, IOptions<CloudinarySettings> cloudinarySettings, IPhotoBusiness photoBusiness, ICityBusiness cityBusiness)
        {
            _mapper = mapper;
            _cloudinarySettings = cloudinarySettings;
            _photoBusiness = photoBusiness;
            _cityBusiness = cityBusiness;

            Account account = new Account(_cloudinarySettings.Value.CloudName, _cloudinarySettings.Value.ApiKey, _cloudinarySettings.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        [HttpPost]
        public IActionResult AddCityPhoto(int cityid, [FromForm]PhotoSaveDto model)
        {
            var city = _cityBusiness.GetAll(x => x.Id == cityid, x => x.Photos).FirstOrDefault();
            if (city == null)
            {
                return BadRequest("City not found");
            }
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (currentUserId != city.UserId)
            {
                return Unauthorized();
            }
            var file = model.File;
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            model.Url = uploadResult.Uri.ToString();
            model.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(model);
            photo.City = city;
            if (!city.Photos.Any(x => x.IsMain))
            {
                photo.IsMain = true;
            }

            city.Photos.Add(photo);

            if (_photoBusiness.SaveAll())
            {
                var photoDto = _mapper.Map<PhotoDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoDto);
            }
            return BadRequest("Not added photo");
        }

        [HttpGet("{id}",Name = "GetPhoto")]
        public IActionResult GetPhoto(int id)
        {
            var photo = _photoBusiness.Get(id);
            var mappedPhoto = _mapper.Map<PhotoDto>(photo);


            return Ok(mappedPhoto);

        }
    }

}