﻿using BilgeAdamEvimiKur.DAL.ContextClasses;
using BilgeAdamEvimiKur.DAL.Repositories.Abstracts;
using BilgeAdamEvimiKur.ENTITIES.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BilgeAdamEvimiKur.DAL.Repositories.Concretes.EF
{
    public class AppRoleRepository : BaseRepository<AppRole>, IAppRoleRepository
    {
        public AppRoleRepository(MyContext db) : base(db)
        {

        }

    }
}
