using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace Polizia
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Agente> agenti = Agente.ElencoAgenti();

            foreach (Agente a in agenti)
                Console.WriteLine(a);


            //Console.WriteLine("Inserire codice area: ");
            //string area = Console.ReadLine();

            //List<Agente> agentiperarea = Agente.AgentiPerArea(area);

            //foreach (Agente a in agentiperarea)
            //    Console.WriteLine(a);



            Console.WriteLine("Inserire anni di servizio: ");
            int anni = int.Parse(Console.ReadLine());
            List<Agente> agentiperanni = Agente.AgentiPerAnni(anni);



            foreach (Agente a in agentiperanni)
                Console.WriteLine(a);

            Console.WriteLine("Inserisci il nome:");
            string nome = Console.ReadLine();
            Console.WriteLine("Inserisci il cognome:");
            string cognome = Console.ReadLine();
            Console.WriteLine("Inserisci il codice fiscale:");
            string cf = Console.ReadLine();
            Console.WriteLine("Inserisci la data di nascita:");
            DateTime data = Convert.ToDateTime(Console.ReadLine());
            Console.WriteLine("Inserisci gli anni di servizio:");
            int anniServizio = Convert.ToInt32(Console.ReadLine());
            Agente.InserisciAgente(cf, nome, cognome, data, anniServizio);

        }
    }
}
