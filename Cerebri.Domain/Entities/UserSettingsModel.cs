using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerebri.Domain.Entities
{
    public class UserSettingsModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public DateTime LastCheckIn { get; set; }
    }
}
