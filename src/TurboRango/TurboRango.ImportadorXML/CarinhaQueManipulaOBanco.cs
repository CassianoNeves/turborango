using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    class CarinhaQueManipulaOBanco
    {
        private string connectionString;
       
        public CarinhaQueManipulaOBanco( string connectionString )
        {
            this.connectionString = connectionString;

        }

        internal void InserirRestaurante(Restaurante restaurante)
        {

            if( restaurante.Contato != null && restaurante.Localizacao != null){
                InserirContato(restaurante.Contato);
                InserirLocalizacao(restaurante.Localizacao);

                using (var connection = new SqlConnection(this.connectionString))
                {
                    string comandoSQL = "INSERT INTO [dbo].[Restaurante] ([Capacidade],[Nome],[Categoria]) VALUES (@Capacidade, @Nome, @Categoria)";
                    using (var inserirRestaurante = new SqlCommand(comandoSQL, connection))
                    {
                        inserirRestaurante.Parameters.Add("@Capacidade", SqlDbType.Int).Value = restaurante.Capacidade;
                        inserirRestaurante.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = restaurante.Nome;
                        inserirRestaurante.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = restaurante.Categoria;

                        connection.Open();
                        int resultado = inserirRestaurante.ExecuteNonQuery();
                    }
                }
            }
        }

        internal void InserirContato(Contato contato)
        {

            using (var connection = new SqlConnection(this.connectionString))
            {
                string comandoSQL = "INSERT INTO [dbo].[contato] ([Site],[Telefone]) VALUES (@Site, @Telefone)";
                using (var inserirContato = new SqlCommand(comandoSQL, connection))
                {
                    inserirContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = contato.Site;
                    inserirContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = contato.Telefone;

                    connection.Open();
                    int resultado = inserirContato.ExecuteNonQuery();
                }
            }  
        }

        internal void InserirLocalizacao(Localizacao localizacao)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                string comandoSQL = "INSERT INTO [dbo].[Localizacao] ([Bairro],[Logradouro],[Latitude],[Longitude]) VALUES (@Bairro, @Logradouro, @Latitude, @Longitude)";
                using (var inserirLocalizacao = new SqlCommand(comandoSQL, connection))
                {
                    inserirLocalizacao.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = localizacao.Bairro;
                    inserirLocalizacao.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = localizacao.Logradouro;
                    inserirLocalizacao.Parameters.Add("@Latitude", SqlDbType.Float).Value = localizacao.Latitude;
                    inserirLocalizacao.Parameters.Add("@Longitude", SqlDbType.Float).Value = localizacao.Longitude;

                    connection.Open();
                    int resultado = inserirLocalizacao.ExecuteNonQuery();
                }
            }
        }

        internal IEnumerable<Contato> getContatos()
        {
            IList<Contato> contatos = new List<Contato>();

            using (var connection = new SqlConnection(this.connectionString))
            {
                string comandoSQL = "SELECT [Site],[Telefone] FROM [dbo].[Contato]";
                using (var lerContatos = new SqlCommand(comandoSQL, connection))
                {
                    connection.Open();

                    var reader = lerContatos.ExecuteReader();

                    while(reader.Read()){
                        string site = reader.GetString(0);
                        string telefone = reader.GetString(1);

                        contatos.Add(new Contato(reader.GetString(0), reader.GetString(1)));                   
                    }
                }
            }

            return contatos;
        }
    }
}
