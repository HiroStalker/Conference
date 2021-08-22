using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Data
{
    public class Speaker : ConferenceDTO.Speaker
    {
        public virtual ICollection<SessionSpeaker> SessionSpeakers {get; set;} = new List<SessionSpeaker>();
    }
}
