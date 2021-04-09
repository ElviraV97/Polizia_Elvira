using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polizia
{
    class Area
    {
        public int IdArea { get; }
        public string Codice { get; }
        public int IdAgente { get; }

        public Area(int id, string codice, int idagente)
        {
            IdArea = id;
            Codice = codice;
            IdAgente = idagente;
        }


    }
}
