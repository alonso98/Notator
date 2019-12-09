using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UpdateNotator.Application.ApplicationServices.Topic
{
    public interface ITopicService
    {
        Task<ServiceResult<TopicDto>> CreateTopic(string userName, string topicName);

        Task<ServiceResult<IEnumerable<TopicDto>>> GetAllTopicsByUser(string userName);

        Task<ServiceResult<TopicDto>> UpdateTopic(Guid topicId, string topicName);

        Task<ServiceResult> DeleteTopic(string userName, Guid topicId);
    }
}
