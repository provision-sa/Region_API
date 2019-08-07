using System.ComponentModel.DataAnnotations;

namespace Region_API.Models
{
    public class Province
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
