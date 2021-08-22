using System.Collections;
using System.Collections.Generic;

namespace Backend.Data
{
    public class Tag : ConferenceDTO.Tag
    {
        public virtual ICollection<SessionTag> SessionTags {get; set;}
    }
}