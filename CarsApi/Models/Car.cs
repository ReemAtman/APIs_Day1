using CarsApi.Validations;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CarsApi.Models
{
    public class Car
    {

        [Range(1, 100, ErrorMessage = "{0} Must be beteen {1} and {2}")]
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} Must be between {2} and {1}")]
        public string Name { get; set; }
        public string Type { get; set; }
        [PastDate]
        public DateTime ProduactionDate { get; set; }

        public Car(int id,string name, DateTime produactionDate )
        {
            Id = id;
            Name = name;
            ProduactionDate = produactionDate;

        }
    }
}
