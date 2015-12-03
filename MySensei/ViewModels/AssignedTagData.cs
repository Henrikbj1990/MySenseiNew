using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MySensei.ViewModels
{
    public class AssignedTagData
    {
        public int TagID { get; set; }
        public string TagName { get; set; }
        public bool Assigned { get; set; }
    }
}