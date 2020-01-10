using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement.Pages.Staff
{

    public class StaffMainPageMasterMenuItem
    {
        public StaffMainPageMasterMenuItem()
        {
            TargetType = typeof(StaffMainPageMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}