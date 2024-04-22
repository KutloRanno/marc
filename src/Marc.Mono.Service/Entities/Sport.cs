using System.ComponentModel.DataAnnotations;

namespace Marc.Mono.Service.Entities;

    public class Sport:IEntity
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        public DateTimeOffset PostDate { get; set; }

        [Url]
        [StringLength(100)]
        public required string ImageUri { get; set; }
    }
