using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UpdateNotator.Domain.Core.Common.Repository;
using UpdateNotator.Domain.Core.Topics;
using UpdateNotator.Domain.Core.Users;

namespace UpdateNotator.Application.ApplicationServices.Topic
{
    public class TopicService : BaseService, ITopicService
    {
        private IUnitOfWork db;
        private IMapper mapper;
        private ILogger<TopicService> logger;

        public TopicService(IUnitOfWork db,
                            IMapper mapper,
                            ILogger<TopicService> logger)
        {
            this.db = db;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ServiceResult<IEnumerable<TopicDto>>> GetAllTopicsByUser(string userName)
        {
            try
            {
                logger.LogTrace("Start", userName);

                var user = await GetUser(userName);
                var topics = await db.Topics.GetWhere(new TopicsByUserSpecification(user.Id));
                return Result(mapper.Map<IEnumerable<TopicDto>>(topics));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<IEnumerable<TopicDto>>(ex);
            }
            finally
            {
                logger.LogTrace("Finish");
            }
        }

        public async Task<ServiceResult<TopicDto>> CreateTopic(string userName, string topicName)
        {
            try
            {
                logger.LogTrace("Start", userName, topicName);

                var user = await GetUser(userName);
                var topic = Domain.Core.Topics.Topic.Create(name: topicName,
                                                          userId: user.Id);
                await db.Topics.Add(topic);
                await db.Save();

                var resultTopic = await db.Topics.GetById(topic.Id);
                return Result(mapper.Map<TopicDto>(resultTopic));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<TopicDto>(ex);
            }
            finally
            {
                logger.LogTrace("Finish");
            }
        }

        public async Task<ServiceResult<TopicDto>> UpdateTopic(Guid topicId, string topicName)
        {
            try
            {
                logger.LogTrace("Start", topicId, topicName);

                var topic = await db.Topics.GetById(topicId);
                topic.ChangeName(topicName);

                await db.Topics.Update(topic);
                await db.Save();

                var newTopic = await db.Topics.GetById(topicId);
                return Result(mapper.Map<TopicDto>(newTopic));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<TopicDto>(ex);
            }
            finally
            {
                logger.LogTrace("Finish");
            }
        }

        public async Task<ServiceResult> DeleteTopic(string userName, Guid topicId)
        {
            try
            {
                logger.LogTrace("Start", userName, topicId);
                var user = await GetUser(userName);
                var topic = await db.Topics.GetWhere(new TopicByUserAndTopicIdSpecification(user.Id, topicId));


                return Result(); //todo
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result(ex);
            }
            finally
            {
                logger.LogTrace("Finish");
            }
        }

        #region private methods
        private Task<Domain.Core.Users.User> GetUser(string userName)
        {
            var user = db.Users.FirstOrDefault(new UserByLoginSpecification(userName));
            if (user == null) throw new UserNotFoundException();

            return user;
        }



        #endregion
    }
}
