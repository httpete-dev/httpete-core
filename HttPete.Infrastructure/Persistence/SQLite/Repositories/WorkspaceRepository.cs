﻿using HttPete.Domain.Interfaces.Repositories;
using HttPete.Model.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttPete.Infrastructure.Persistence.SQLite.Repositories
{
    public class WorkspaceRepository : BaseRepository<Workspace>, IWorkspaceRepository
    {
        public WorkspaceRepository(AppDbContext context) : base(context) { }
    }
}
