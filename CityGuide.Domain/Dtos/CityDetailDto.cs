using System;
using System.Collections.Generic;
using System.Text;
using CityGuide.Domain.Entity;

namespace CityGuide.Domain.Dtos
{
    public class CityDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public List<Photo> Photos { get; set; }
    }
}
