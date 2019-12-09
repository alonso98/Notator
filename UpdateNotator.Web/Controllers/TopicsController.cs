using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpdateNotator.Application.ApplicationServices.Topic;
using UpdateNotator.Application.ApplicationServices.User;

namespace UpdateNotator.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TopicsController : ControllerBase
    {
        private ITopicService topicService;

        public TopicsController(ITopicService topicService)
        {
            this.topicService = topicService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var topicsResult = await topicService.GetAllTopicsByUser(User.Identity.Name);
            if (topicsResult.IsSuccessed) return Ok(topicsResult.Data);

            throw topicsResult.Exception;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromForm]string nameOfTopic)
        {
            nameOfTopic = nameOfTopic ?? throw new ArgumentNullException(nameof(nameOfTopic));

            var topicResult = await topicService.CreateTopic(User.Identity.Name, nameOfTopic);
            if (topicResult.IsSuccessed) return CreatedAtAction(nameof(CreateTopic), topicResult.Data);

            throw topicResult.Exception;
        }

        [HttpPut]
        public async Task<IActionResult> ChangeTopic([FromForm]TopicDto topicDto)
        {
            topicDto = topicDto ?? throw new ArgumentNullException(nameof(topicDto));
            topicDto.Id = topicDto.Id ?? throw new ArgumentNullException(nameof(topicDto.Id));
            topicDto.Name = topicDto.Name ?? throw new ArgumentNullException(nameof(topicDto.Name));

            var topicResult = await topicService.UpdateTopic(topicDto.Id.Value, topicDto.Name);
            if (topicResult.IsSuccessed) return Ok(topicResult.Data);

            throw topicResult.Exception;
        }

        //[HttpDelete]
        //public async Task<IActionResult> DeleteTopic(Guid topicId)
        //{
        //    ///var result = await topicService.DeleteTopic();
        //}
    }
}