using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorBookWebApp.DTOs.GenreDTOs
{
    public class GenreDto
    {
        public int Id { get; set; }

        [StringLength(125)]
        public string Name { get; set; }
    }
}
