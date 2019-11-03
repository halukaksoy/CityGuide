using System;
using System.Collections.Generic;
using System.Text;

namespace CityGuide.Domain.Dtos
{
    public class CityListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public List<PhotoDto> Photos { get; set; }
    }
}
