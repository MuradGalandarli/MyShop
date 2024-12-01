using BusinessLayer.Service;
using DataAccessLayer.Abstract;
using EntityLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Manager
{
    public class ContactManager : IContactService
    {
        private readonly IContact _contact;
        private readonly IEmailService _emailService;
        public ContactManager(IContact _contact,
            IEmailService emailService)
        {
            this._contact = _contact;
            _emailService = emailService;
        }
        public async Task<bool> Add(Contact t)
        {
            if (!string.IsNullOrEmpty(t.Email))
            {
                bool sendMail = await _emailService.SendMail(t.Email,t.Subject,t.Message);
                if (sendMail)
                {
                    var IsSucces = await _contact.Add(t);
                    return IsSucces;
                }
            }
            return false;
        }

        public async Task<bool> Delete(int id)
        {
            var IsSucces = await _contact.Delete(id);
            return IsSucces;
        }

        public async Task<List<Contact>> GetAll()
        {
            var result = await _contact.GetAll();
            return result;
        }

        public async Task<Contact> GetById(int id)
        {
            var result = await _contact.GetById(id);
            return result;
        }

        public async Task<bool> Update(Contact t)
        {
            var IsSucces = await _contact.Update(t);
            return IsSucces;
        }
    }
}
