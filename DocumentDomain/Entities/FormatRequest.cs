using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentDomain.Entities
{
    public class FormatRequest
    {
        [Required]
        public string? Xml { get; set; }

    }
}
