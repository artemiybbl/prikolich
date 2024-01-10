﻿using Microsoft.EntityFrameworkCore;
using Laba4.Models;

namespace Laba4.Data
{
    public class ApiContext : DbContext
    {
        public DbSet<Contacts> Contacts { get; set; }

        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {

        }
    }
}
