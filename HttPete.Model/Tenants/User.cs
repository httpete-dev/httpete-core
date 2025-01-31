using Abp.Domain.Entities;

namespace HttPete.Model.Tenants
{
    public class User : Entity, IEntity
    {
        public int? OrganizationId { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public string? OAuthProvider { get; set; }
        public string? OAuthProviderAccountId { get; set; }
        public string? Image { get; set; }

        public IEnumerable<Workspace>? Workspaces { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastLogin { get; set; }

        public User()
        {

        }
    }
}