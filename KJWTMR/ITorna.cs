using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KJWTMR
{
    enum Stilus
    {
        Eronleti = 1, Kardio = 0, Kimelo = 2
    }
    interface ITorna
    {
        int OraBer { get; set; } //forint
        int Idotartam { get; set; } //perc
        Stilus Stilus { get; set; }
    }
}
