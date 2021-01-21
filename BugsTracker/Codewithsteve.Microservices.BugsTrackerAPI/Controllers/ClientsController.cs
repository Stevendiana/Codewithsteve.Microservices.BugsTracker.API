using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Codewithsteve.Microservices.BugsTracker.API.Controllers.Resources;
using Codewithsteve.Microservices.BugsTracker.API.Data.Repositories;
using AutoMapper;
using Codewithsteve.Microservices.BugsTracker.Models;
using System.IO;

namespace Codewithsteve.Microservices.BugsTracker.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    //[Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public ClientsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {

            var item = _unitOfWork.Clients.SingleOrDefault(b => b.ClientId == id);

            if (item == null)
                return NotFound("Item not found");

            var itemData = _mapper.Map<Client, ClientViewModel>(item);

            return Ok(itemData);
        }


        [HttpGet]
        public ActionResult GetClients()
        {
           
            var items = _unitOfWork.Clients.GetAllClients();

            if (items == null)
                return null;

            var itemsData = _mapper.Map<IEnumerable<Client>, IEnumerable<ClientViewModel>>(items);

            return Ok(itemsData);

        }


        // PUT: api/Clients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(string id, [FromBody]  ClientData client)
        {
            if (!ModelState.IsValid) // return validation error if client side validation is not passed.
            {
                return BadRequest(ModelState);
            }

            try
            {
                var item = _unitOfWork.Clients.SingleOrDefault(x => x.ClientId == id);

                if (item == null)
                    return NotFound();

                _mapper.Map<ClientData, Client>(client, item);
                item.Name = client.Name;
                item.DateModified = DateTime.UtcNow;

                item.ClientCode = "CUS" + "-" + PartId(id, 8).ToUpper();


                _unitOfWork.Clients.Update(item);

                await _unitOfWork.CompleteAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var items = _unitOfWork.Clients.GetAllClients();

            if (items == null)
                return null;

            var itemsData = _mapper.Map<IEnumerable<Client>, IEnumerable<ClientViewModel>>(items);

            return Ok(itemsData);
        }

        // POST: api/Clients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient([FromBody] ClientData client)
        {
            if (!ModelState.IsValid) // return validation error if client side validation is not passed.
            {
                return BadRequest(ModelState);
            }

            try
            {
                var Path = "App_Data/uploads/clients";
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
               

                var id = Guid.NewGuid().ToString();
                var newClient = new Client
                {
                    Name = client.Name,
                    DateCreated = DateTime.UtcNow,
                    Description = client.Description,

                };
               

                newClient.ClientCode = "CLAPP" + "-" + PartId(id, 8).ToUpper();
                _unitOfWork.Clients.Add(newClient);

                string[] ClientInfo = { id, client.Name, client.Description };
                System.IO.File.WriteAllLines(Path + "/Client-" + newClient.ClientCode + ".txt", ClientInfo);

                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientExists(client.ClientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            var items = _unitOfWork.Clients.GetAllClients();

            if (items == null)
                return null;

            var itemsData = _mapper.Map<IEnumerable<Client>, IEnumerable<ClientViewModel>>(items);

            return Ok(itemsData);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> DeleteClient(string id)
        {
            var client = _unitOfWork.Clients.SingleOrDefault(x=>x.ClientId==id);
            if (client == null)
            {
                return NotFound();
            }

            _unitOfWork.Clients.Remove(client);
            await _unitOfWork.CompleteAsync();

            return client;
        }

        private bool ClientExists(string id)
        {
            return _unitOfWork.Clients.Any(e => e.ClientId == id);
        }

        private string PartId(string param, int length)
        {
            if (param == null)
            {
                return null;
            }
            string result = param.Substring(0, length);
            return result;
        }
    }
}
