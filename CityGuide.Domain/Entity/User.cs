using System;
using System.Collections.Generic;
using System.Text;
using CityGuide.Domain.Interface;

namespace CityGuide.Domain.Entity
{
  public class User :IEntity
    {
        public User()
        {
            Cities = new List<City>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public List<City> Cities { get; set; }
    }
}
