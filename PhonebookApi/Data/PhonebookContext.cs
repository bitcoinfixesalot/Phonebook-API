using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhonebookApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhonebookApi.Data
{
    public class PhonebookContext : IdentityDbContext<IdentityUser>
    {
        public PhonebookContext(DbContextOptions<PhonebookContext> options) : base(options)
        {
        }

        public DbSet<PhoneContact> PhoneContacts { get; set; }
    }
}
