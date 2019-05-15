using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technical_Task.Data.DTO
{
    [Table("TTCountry")]
    public class Country
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedDateUtc { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
