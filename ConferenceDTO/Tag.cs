using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace ConferenceDTO
{
    public class Tag
    {
        public int ID { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}