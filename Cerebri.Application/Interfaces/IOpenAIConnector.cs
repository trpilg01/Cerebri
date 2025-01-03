using Cerebri.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerebri.Application.Interfaces
{
    public interface IOpenAIConnector
    {
        Task<OpenAIResponseModel> Prompt(IList<JournalEntryModel> journals);
    }
}
