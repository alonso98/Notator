using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UpdateNotator.Application.ApplicationServices.Entry
{
    public interface IEntryService
    {
        Task<ServiceResult<EntryDto>> CreateEntry(string userName, Guid topicId, EntryDto innerEntry);

        Task<ServiceResult<IEnumerable<EntryDto>>> GetEntries(string userName, Guid topicId);
    }
}
