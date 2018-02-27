using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PhonebookApi.Data.Entities;

namespace PhonebookApi.Data
{
    public class PhonebookRepository : IPhonebookRepository
    {
        private readonly PhonebookContext _ctx;

        public PhonebookRepository(PhonebookContext ctx)
        {
            this._ctx = ctx;
        }

        public void AddContact(PhoneContact newContact)
        {
            _ctx.Add(newContact);
        }

        public void Delete(int id)
        {
            var item = GetContactById(id);
            _ctx.PhoneContacts.Remove(item);
        }

        public IEnumerable<PhoneContact> GetAllContacts()
        {
            return _ctx.PhoneContacts.OrderBy(a => a.Name).ToList();
        }

        public PhoneContact GetContactById(int id)
        {
            return _ctx.PhoneContacts.Where(a => a.Id == id).FirstOrDefault();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
