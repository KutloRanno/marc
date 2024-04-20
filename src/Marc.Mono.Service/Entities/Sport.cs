using System.ComponentModel.DataAnnotations;

namespace Marc.Mono.Service.Entities;

    public class Sport
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        public DateTime PostDate { get; set; }

        [Url]
        [StringLength(100)]
        public required string ImageUri { get; set; }
    }
