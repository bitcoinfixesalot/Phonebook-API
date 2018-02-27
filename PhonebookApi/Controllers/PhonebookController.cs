using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhonebookApi.Data;
using PhonebookApi.Data.Entities;
using PhonebookApi.ViewModels;

namespace PhonebookApi.Controllers
{
    [Produces("application/json")]
    [Route("api/phonebook")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PhonebookController : Controller
    {
        private readonly IPhonebookRepository _repository;
        private readonly ILogger<PhonebookRepository> _logger;
        private readonly IMapper _mapper;

        public PhonebookController(IPhonebookRepository phonebookRepository, ILogger<PhonebookRepository> logger, IMapper mapper)
        {
            this._repository = phonebookRepository;
            this._logger = logger;
            this._mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllContacts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get contacts: {ex}");
                return BadRequest("Failed to get contacts");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var contact = _repository.GetContactById(id);

                if (contact != null) return Ok(_mapper.Map<PhoneContact, ContactViewModel>(contact));
                else return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get contact: {ex}");
                return BadRequest("Failed to get contact");
            }
        }

        // POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        [HttpPost]
        public IActionResult Post([FromBody]ContactViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newPhoneContact = _mapper.Map<ContactViewModel, PhoneContact>(model);

                    _repository.AddContact(newPhoneContact);
                    if (_repository.SaveAll())
                    {
                        return Created($"/api/phonebook/{newPhoneContact.Id}", _mapper.Map<PhoneContact, ContactViewModel>(newPhoneContact));
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save a new contact: {ex}");
            }

            return BadRequest("Failed to save new contact");
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]ContactViewModel model)
        {
            try
            {
                if (model == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid phone contact");
                }
                var existingItem = _repository.GetContactById(model.Id);
                if (existingItem == null)
                {
                    return NotFound("Phone contact not found");
                }
                existingItem.Name = model.Name;
                existingItem.Address = model.Address;
                existingItem.PhoneNumber = model.PhoneNumber;
                _repository.SaveAll();
            }
            catch (Exception)
            {
                return BadRequest("Failed to update contact");
            }
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var item = _repository.GetContactById(id);
                if (item == null)
                {
                    return NotFound("Contact not found");
                }
                _repository.Delete(id);
                _repository.SaveAll();
            }
            catch (Exception)
            {
                return BadRequest("Failed to delete contact");
            }
            return NoContent();
        }
    }
}
