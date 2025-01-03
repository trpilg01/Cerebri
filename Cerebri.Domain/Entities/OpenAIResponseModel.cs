using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerebri.Domain.Entities
{
    public class OpenAIResponseModel
    {
        public string Summary { get; set; }
        public string Insights { get; set; }
        
        public OpenAIResponseModel(string summary, string insights)
        {
            Summary = summary;
            Insights = insights;
        }
    }
}
