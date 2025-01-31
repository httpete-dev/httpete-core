using Abp.Domain.Entities;

namespace HttPete.Model.Tenants
{
    public class OrgAccess : Entity, IEntity
    {
        public int OrgId { get; set; }

        public int UserId { get; set; }

        public string Workspaces { get; set; }

        public OrgAccess() { }

        public static OrgAccess Grant(int userId, int orgId, int[] workspaces)
            => new OrgAccess
            {
                Workspaces = string.Join(",", workspaces),
                OrgId = orgId,
                UserId = userId,
            };
    }
}