using Abp.Domain.Entities;
using HttPete.Model.Tenants;

namespace HttPete.Model
{
    public class Organization : Entity, IEntity
    {
        public string Name { get; set; }

        public DateTime Created { get; set; }

        public IEnumerable<Workspace> Workspaces { get; set; }

        public IEnumerable<User> Users { get; set; }

        public Organization() { }

        public Organization(User user) 
        {
            Created = DateTime.UtcNow;
            Name = user.Name + "\'s Organization";
        }
    }
}