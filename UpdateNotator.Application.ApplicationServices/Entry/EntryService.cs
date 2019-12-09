using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UpdateNotator.Domain.Core.Common.Repository;
using UpdateNotator.Domain.Core.Entries;
using UpdateNotator.Domain.Core.Topics;
using UpdateNotator.Domain.Core.Users;

namespace UpdateNotator.Application.ApplicationServices.Entry
{
    public class EntryService : BaseService, IEntryService
    {
        private IUnitOfWork db;
        private IMapper mapper;
        private ILogger<IEntryService> logger;

        public EntryService(IUnitOfWork db,
                           IMapper mapper,
                           ILogger<IEntryService> logger)
        {
            this.db = db;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<ServiceResult<EntryDto>> CreateEntry(string userName, Guid topicId, EntryDto innerEntry)
        {
            try
            {
                logger.LogTrace("Start", userName, topicId, innerEntry);

                var user = await GetUser(userName);
                var topic = await GetTopic(user.Id, topicId);

                var entry = Domain.Core.Entries.Entry.Create(head: innerEntry.Head,
                                                             text: innerEntry.Text,
                                                          topicId: topicId,
                                                             type: innerEntry.Type);
                //ToDo: добавить добавление ссылок
                innerEntry.Links.ForEach(m =>
                {
                    entry.AddLink(m.Url, m.NameUrl);
                });

                await db.Entries.Add(entry);
                await db.Save();

                var newEntry = await db.Entries.GetById(entry.Id);
                return Result(mapper.Map<EntryDto>(newEntry));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<EntryDto>(ex);
            }
            finally
            {
                logger.LogTrace("Finish");
            }
        }

        public async Task<ServiceResult<IEnumerable<EntryDto>>> GetEntries(string userName, Guid topicId)
        {
            try
            {
                logger.LogTrace("Start", userName, topicId);

                var user = await GetUser(userName);
                var topic = await GetTopic(user.Id, topicId);
                var entries = await db.Entries.GetWhere(new EntriesByTopicIdSpecification(topic.Id));

                return Result(mapper.Map<IEnumerable<EntryDto>>(entries));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Result<IEnumerable<EntryDto>>(ex);
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

        private Task<Domain.Core.Topics.Topic> GetTopic(Guid userId, Guid topicId)
        {
            var topic = db.Topics.FirstOrDefault(new TopicByUserAndTopicIdSpecification(userId, topicId));
            if (topic == null) throw new TopicNotFoundException();

            return topic;
        }

        #endregion
    }
}
