using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerebri.Domain.Entities
{
    public class OpenAIResponseModel
    {
        public String Summary { get; set; }
        public String Insights { get; set; }
        
        public OpenAIResponseModel(string summary, string insights)
        {
            Summary = summary;
            Insights = insights;
        }
    }
}
