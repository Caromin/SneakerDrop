using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using dm = SneakerDrop.Domain.Models;

namespace SneakerDrop.Code
{
    public class SneakerDropDbContext : DbContext
    {
        public DbSet<dm.User> Users;

          //modelbuilder.hasdefaultschema("User")
    }
}
