using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KJWTMR
{
    abstract class SzemelyiEdzo : ITorna
    {
        protected SzemelyiEdzo(int oraBer, int idotartam, Stilus stilus)
        {
            OraBer = oraBer;
            Idotartam = idotartam;
            Stilus = stilus;
        }

        public int OraBer { get; set; }
        public int Idotartam { get; set; }
        public Stilus Stilus { get; set; }
    }
}
