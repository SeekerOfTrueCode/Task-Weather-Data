using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technical_Task.Data.DTO
{
    [Table("TTCity")]
    public class City
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateUtc { get; set; }

        public long CountryId { get; set; }
        public Country Country { get; set; }
    }
}
