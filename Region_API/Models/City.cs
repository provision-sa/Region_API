using System.ComponentModel.DataAnnotations;

namespace Region_API.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string Description { get; set; }

        public virtual Province Province { get; set; }
    }
}
