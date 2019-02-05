using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SneakerDrop.Domain.Models
{
    [Table("Type", Schema = "Product")]
    public class Type
    {

        public int TypeId { get; set; }


        [StringLength(50)]
        [Required]
        public string TypeName { get; set; }
    }
}

