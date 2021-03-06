using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Attendee>()
            .HasIndex(a => a.UserName)
            .IsUnique();

            modelBuilder.Entity<Session>().Ignore(s => s.Duration);

            modelBuilder.Entity<ConferenceAttendee>().HasKey(ca => new { ca.ConferenceID, ca.AttendeeID });



            // Many-to-many: Session <-> Attendee
            modelBuilder.Entity<SessionAttendee>()
                .HasKey(ca => new { ca.SessionID, ca.AttendeeID });


            // Many-to-many: Speaker <-> Session
            modelBuilder.Entity<SessionSpeaker>()
                .HasKey(ss => new { ss.SessionId, ss.SpeakerId });

            modelBuilder.Entity<SessionTag>().HasKey(ST => new { ST.TagID, ST.SessionID });
        }


        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Conference> Conferences { get; set; }

        public DbSet<Attendee> Attendees { get; set; }



    }
}