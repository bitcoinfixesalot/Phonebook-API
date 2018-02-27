using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using PhonebookApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhonebookApi.Data
{
    public class PhonebookSeeder
    {
        private readonly PhonebookContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<IdentityUser> _userManager;

        public PhonebookSeeder(PhonebookContext phonebookContext, IHostingEnvironment hosting, UserManager<IdentityUser> userManager)
        {
            this._ctx = phonebookContext;
            this._hosting = hosting;
            this._userManager = userManager;
        }

        public async Task Seed()
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.PhoneContacts.Any())
            {
                //create sample data

                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/sampleData.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<PhoneContact>>(json);
                _ctx.PhoneContacts.AddRange(products);
            }
            await CreateDefaultUserAsync();
        }

        public async Task CreateDefaultUserAsync()
        {
            var user = await _userManager.FindByNameAsync("admin");

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = "admin",
                    Email = "admin@localhost"
                };

                IdentityResult result = await _userManager.CreateAsync(user, "P@ssw0rd!");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user");
                }
            }
        }
    }
}
