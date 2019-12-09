using System;
using System.Collections.Generic;
using System.Text;
using UpdateNotator.Domain.Core.Entries;

namespace UpdateNotator.Application.ApplicationServices.Entry
{
    public class EntryDto
    {
        public string Text { get; set; }

        public string Head { get; set; }

        public DateTime? CreationTime { get; set; }

        public EntryTypes Type { get; set; }

        public List<LinkDto> Links { get; set; }
    }
}
