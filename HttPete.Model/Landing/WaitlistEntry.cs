using Abp.Domain.Entities;

namespace HttPete.Model.Landing
{
    /// <summary>
    /// Represents a user who has signed up for the waitlist.
    /// </summary>
    public class WaitlistEntry : Entity, IEntity
    {
        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; set; } = "John Doe";

        /// <summary>
        /// The email of the user.
        /// </summary>
        public string Email { get; set; } = "john.doe@httpete.dev";

        /// <summary>
        /// The IP address of the client that signed up.
        /// </summary>
        public string? ClientIp { get; set; }

        /// <summary>
        /// The date and time the user signed up.
        /// </summary>
        public DateTime SignUpDate { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public WaitlistEntry()
        {
            SignUpDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Constructor for creating a new waitlist entry.
        /// </summary>
        /// <param name="dto"></param>
        public WaitlistEntry(WaitlistEntryDTO dto)
        {
            Name = dto.Name;
            Email = dto.Email;
            ClientIp = dto.ClientIp;
            SignUpDate = DateTime.UtcNow;
        }
    }
}