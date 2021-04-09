using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polizia
{
    class Persona
    {
        public string Cognome { get; }
        public string Nome { get; }
        public string CF { get; }

        public Persona(string cf, string nome, string cognome)
        {
            CF = cf;
            Nome = nome;
            Cognome = cognome;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;

            return CF == ((Persona)obj).CF;
        }
    }
}
