using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polizia
{
    class Agente : Persona
    {
        public int IdAgente { get; }
        public int AnniServizio { get; }
        public DateTime DataNascita { get; }
        public Agente(int id, string cf, string nome, string cognome, DateTime datanascita, int anniServizio)
            : base(cf, nome, cognome)
        {
            IdAgente = id;
            DataNascita = datanascita;
            AnniServizio = anniServizio;

        }

        static string connectionString = ConfigurationManager.ConnectionStrings["Polizia"].ConnectionString;

        public static List<Agente> ElencoAgenti()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("Select * from Agenti", conn))
            {
                List<Agente> agenti = new List<Agente>();

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    agenti.Add(new Agente((int)reader["IdAgente"], reader["CF"].ToString(), reader["Nome"].ToString(),
                        reader["Cognome"].ToString(), (DateTime)reader["DataNascita"], (int)reader["AnniServizio"]));

                conn.Close();
                return agenti;
            }
        }

        public static List<Agente> AgentiPerAnni(int anni)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("Select * from Agenti where AnniServizio >= @anni", conn))
            {
                List<Agente> agentiperanni = new List<Agente>();

                conn.Open();
                cmd.Parameters.AddWithValue("@anni", anni);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    agentiperanni.Add(new Agente((int)reader["IdAgente"], reader["CF"].ToString(), reader["Nome"].ToString(),
                        reader["Cognome"].ToString(), (DateTime)reader["DataNascita"], (int)reader["AnniServizio"]));

                return agentiperanni;
            }
        }

        public static List<Agente> AgentiPerArea(string area)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("Select * from Agenti inner join Assegnazione on Agenti.IdAgente = Assegnazione.IdAgente" +
                "inner join AreeMetropolitane on Assegnazione.IdArea = AreeMetropolitane.IdArea" +
                "where AreeMetropolitane.Codice = @area", conn))
            {

                List<Agente> agentiperarea = new List<Agente>();

                conn.Open();
                cmd.Parameters.AddWithValue("@area", area);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    agentiperarea.Add(new Agente((int)reader["IdAgente"], reader["CF"].ToString(), reader["Nome"].ToString(),
                        reader["Cognome"].ToString(), (DateTime)reader["DataNascita"], (int)reader["AnniServizio"]));

                return agentiperarea;
            }
        }

        public static Agente InserisciAgente(string cf, string nome, string cognome, DateTime datanascita, int anniservizio)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlDataAdapter da = new SqlDataAdapter ("Select * from Agenti", conn))
            {
                DataSet ds = new DataSet();

                da.Fill(ds, "Agenti");

                DataTable tabellaAgenti= ds.Tables["Agenti"];

                tabellaAgenti.Rows.Add(0, nome, cognome, cf, datanascita, anniservizio);

                SqlCommandBuilder cb = new SqlCommandBuilder(da);

                conn.Open();
                da.Update(tabellaAgenti);
                SqlCommand cmd = new SqlCommand("select @@identity", conn);

                int id = (int)(decimal)cmd.ExecuteScalar();
                conn.Close();

                return new Agente(id, nome, cognome, cf, datanascita, anniservizio);
            }
        }


        public override string ToString()
        {
            return $"{CF}, {Nome}, {Cognome}, {AnniServizio}";
        }

        
    }
}

