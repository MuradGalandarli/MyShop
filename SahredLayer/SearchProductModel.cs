using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SahredLayer
{
   public class SearchProductModel
    {
        public string? ProductName { get; set; }
        public string? Brand { get; set; }
        public int EndPrice  { get; set; }
        public int StartPrice { get; set; }
    }
}
