using System;
using System.Collections.Generic;
using System.Text;
using CityGuide.Domain.Interface;

namespace CityGuide.Domain.Entity
{
    public class City : IEntity
    {
        public City()
        {
            Photos = new List<Photo>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }

        public List<Photo> Photos { get; set; }
        public User User { get; set; }
    }
}
