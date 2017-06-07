using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDomain.DomainModel
{
    public class TaskStatusCountDm
    {
        public long pending { get; set; }
        public long inprogress { get; set; }
        public long completed { get; set; }
        public long total { get; set; }
    }
}
