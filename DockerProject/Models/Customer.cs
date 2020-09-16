using System.ComponentModel.DataAnnotations;

namespace DockerProject.Models
{
    public partial class Customer
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public int Age { get; set; }
        [StringLength(150)]
        public string City { get; set; }
        [StringLength(150)]
        public string State { get; set; }
        [StringLength(150)]
        public string Country { get; set; }
    }
}
