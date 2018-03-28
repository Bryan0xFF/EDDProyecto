using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolB
{
    public interface ITextoTamañoFijo
    {
        int FixedSizeText { get; }
        string ToFixedSizeString();
    }
}
