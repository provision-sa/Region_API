using System.ComponentModel.DataAnnotations;

namespace Region_API.Models
{
    public class Suburb
    {
        [Key]
        public int Id { get; set; }
        public int CityId { get; set; }
        public string Description { get; set; }

        public virtual City City { get; set; }
    }
}
