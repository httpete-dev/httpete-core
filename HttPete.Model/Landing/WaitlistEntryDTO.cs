namespace HttPete.Model.Landing
{
    public class WaitlistEntryDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string? ClientIp { get; set; }

        public WaitlistEntryDTO(string name, string email, string? ip)
        {
            Name = name;
            Email = email;
            ClientIp = ip;
        }
    }
}