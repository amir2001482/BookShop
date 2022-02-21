using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Models.ViewModels
{
    public class TreeViewCategory
    {
        public TreeViewCategory()
        {
            subs = new List<TreeViewCategory>();
        }
        public int id { get; set; }
        public string title { get; set; }
        public List<TreeViewCategory> subs { get; set; }
    }
}
