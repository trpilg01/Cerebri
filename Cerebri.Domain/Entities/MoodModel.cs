using Cerebri.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerebri.Domain.Entities
{
    public class MoodModel
    {
        [Required]
        [Range(0, 101)]
        public int Id { get; set; }
        public string Name { get; set; }
        public eMoodType Type { get; set; }
        public MoodModel() { }
    }
}
