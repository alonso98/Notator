using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpdateNotator.Application.ApplicationServices.Entry;

namespace UpdateNotator.Web.Controllers
{
    [Route("api/Topics/{topicId}/[controller]")]
    [ApiController]
    [Authorize]
    public class EntriesController : ControllerBase
    {
        private IEntryService entryService;
        public EntriesController(IEntryService entryService)
        {
            this.entryService = entryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEntries(Guid topicId)
        {
            var result = await entryService.GetEntries(User.Identity.Name, topicId);
            if(result.IsSuccessed) return Ok(result.Data);

            throw result.Exception;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntry(Guid topicId, [FromBody]EntryDto newEntry)
        {
            var result = await entryService.CreateEntry(User.Identity.Name, topicId, newEntry);
            if (result.IsSuccessed) return Ok(result.Data);

            throw result.Exception;
        }

        [HttpPut]
        public IActionResult Update()
        {
            throw new NotImplementedException();
        }
    }
}