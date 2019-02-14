using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsIntro.Command
{
    public class ChangeTaskCommand : ICommand
    {
        public int TaskId { get; set; }
        public string UserName { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}