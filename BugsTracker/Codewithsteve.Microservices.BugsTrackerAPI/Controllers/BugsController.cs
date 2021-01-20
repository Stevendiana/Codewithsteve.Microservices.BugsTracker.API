using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Myairops.Tech.Test.Microservices.ClientDatabase.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Myairops.Tech.Test.Microservices.ClientDatabase.API.Controllers;
using System.Security.Claims;
using Myairops.Tech.Test.Microservices.ClientDatabase.API.Data.Repositories;
using AutoMapper;
using Myairops.Tech.Test.Microservices.ClientDatabase.API.Controllers.Resources;
using System.IO;
using Myairops.Tech.Test.ClientDatabase.Models;

namespace Myairops.Tech.Test.Microservices.ClientDatabase.API.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    //[Authorize]
    public class BugsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //


        public BugsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       
        [HttpGet("{id}")]
        public ActionResult Get(string id)
        {

            var item = _unitOfWork.Bugs.SingleOrDefault(b => b.BugId == id);

            if (item == null)
                return NotFound("Item not found");

            var itemData = _mapper.Map<Bug, BugViewModel>(item);

            return Ok(itemData);
        }


        [HttpGet]
        public ActionResult GetBugs()
        {

            var items = _unitOfWork.Bugs.GetAllBugs();

            if (items == null)
                return null;

            var itemsData = _mapper.Map<IEnumerable<Bug>, IEnumerable<BugViewModel>>(items);

            return Ok(itemsData);

        }


        // PUT: api/Bugs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBug(string id, [FromBody] BugData bug)
        {
            if (!ModelState.IsValid) // return validation error if client side validation is not passed.
            {
                return BadRequest(ModelState);
            }

            try
            {
                var item = _unitOfWork.Bugs.SingleOrDefault(x => x.BugId == id);

                if (item == null)
                    return NotFound();

                _mapper.Map<BugData, Bug>(bug, item);
                item.Name = bug.Name;
                item.DateModified = DateTime.UtcNow;

                item.BugCode = "BUG" + "-" + PartId(id, 8).ToUpper();


                _unitOfWork.Bugs.Update(item);

                await _unitOfWork.CompleteAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BugExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var items = _unitOfWork.Bugs.GetAllBugs();

            if (items == null)
                return null;

            var itemsData = _mapper.Map<IEnumerable<Bug>, IEnumerable<BugViewModel>>(items);

            return Ok(itemsData);
        }

        // POST: api/Clients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bug>> PostBug([FromBody] BugData bug)
        {
            if (!ModelState.IsValid) // return validation error if client side validation is not passed.
            {
                return BadRequest(ModelState);
            }

            try
            {
                var Path = "App_Data/uploads/bugs";
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
                var id = Guid.NewGuid().ToString();
                var newBug = new Bug
                {
                    Name = bug.Name,
                    BugId = id,
                    Status = bug.Status,
                    ClientId = bug.ClientId,
                    ResolveByDate = bug.ResolveByDate,
                    severitylevel = bug.severitylevel,

                    Description = bug.Description,
                    Priority = bug.Priority,
                    Impact = bug.Impact,
                    Severity = bug.Severity,
                    Notes = bug.Notes,
                    ResolvedBy = bug.ResolvedBy,
                    SignedoffBy = bug.SignedoffBy,
                    DateCreated = DateTime.UtcNow,

                };

                newBug.BugCode = "BUG" + "-" + PartId(id, 8).ToUpper();
                _unitOfWork.Bugs.Add(newBug);

                string[] BugInfo = { id, bug.Name, bug.Description, bug.ResolveByDate.ToString() };
                System.IO.File.WriteAllLines(Path + "/Bug-" + newBug.BugCode + ".txt", BugInfo);

                await _unitOfWork.CompleteAsync();
            }
            catch (DbUpdateException)
            {
                if (BugExists(bug.BugId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            var items = _unitOfWork.Bugs.GetAllBugs();

            if (items == null)
                return null;

            var itemsData = _mapper.Map<IEnumerable<Bug>, IEnumerable<BugViewModel>>(items);

            return Ok(itemsData);
        }

        // DELETE: api/Bugs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bug>> DeleteBug(string id)
        {
            var bug = _unitOfWork.Bugs.SingleOrDefault(x => x.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }

            _unitOfWork.Bugs.Remove(bug);
            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        


        private bool BugExists(string id)
        {
            return _unitOfWork.Bugs.Any(e => e.BugId == id);
        }

        private string PartId(string param, int length)
        {
            if (param==null)
            {
                return null;
            }
            string result = param.Substring(0, length);
            return result;
        }
    }
}
