using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectAzeriMed.Application.Models.Identity
{
    public class CustomToken
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
