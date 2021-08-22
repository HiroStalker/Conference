using System.Collections.Generic;
using ConferenceDTO;

namespace Backend.Data
{
    public class Session : ConferenceDTO.Session
    {
        public Conference Conference { get; set;}

        public virtual ICollection<SessionSpeaker> SessionSpeakers {get; set;}

        public Track Track {get; set;}

        public virtual ICollection<SessionTag> SessionTag {get; set;}
    }
}