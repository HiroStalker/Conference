using System.Linq;

namespace Backend.Data
{
    public static class EntityExtension
    {
         public static ConferenceDTO.SpeakerResponse MapSpeakerResponse(this Speaker speaker) =>
            new ConferenceDTO.SpeakerResponse
            {
                ID = speaker.ID,
                Name = speaker.Name,
                Bio = speaker.Bio,
                WebSite = speaker.WebSite,
                Sessions = speaker.SessionSpeakers?
                    .Select(ss =>
                        new ConferenceDTO.Session
                        {
                            Id = ss.SessionId,
                            Title = ss.Session.Title
                        })
                    .ToList()
            };
    }
}