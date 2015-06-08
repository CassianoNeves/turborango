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
    class Restaurantes
    {
        readonly static string INSERT_RESTAURANTE = "INSERT INTO [dbo].[Restaurante] ([Capacidade],[Nome],[Categoria],[ContatoId],[LocalizacaoId]) VALUES (@Capacidade, @Nome, @Categoria, @idContato, @idLocalizacao)";
        readonly static string INSERT_CONTATO = "INSERT INTO [dbo].[contato] ([Site],[Telefone]) VALUES (@Site, @Telefone); SELECT @@IDENTITY";
        readonly static string INSERT_LOCALIZACAO = "INSERT INTO [dbo].[Localizacao] ([Bairro],[Logradouro],[Latitude],[Longitude]) VALUES (@Bairro, @Logradouro, @Latitude, @Longitude); SELECT @@IDENTITY";
        
        readonly static string SELECT_CONTATO = "SELECT [Site],[Telefone] FROM [dbo].[Contato]";
        readonly static string SELECT_RESTAURANTES = "SELECT r.Capacidade, r.Nome, r.Categoria, " +
            "c.Site, c.Telefone, l.Bairro, l.Logradouro, l.Latitude, l.Longitude " + 
            "FROM Restaurante r " +
	        "LEFT JOIN Contato c on r.ContatoId = c.Id " +
	        "LEFT JOIN Localizacao l on r.LocalizacaoId = l.Id";

        readonly static string SELECT_IDCONTATO_IDLOCALIZACAO = "SELECT [ContatoId],[LocalizacaoId] FROM [dbo].[Restaurante] WHERE [Id] = @Id";
        readonly static string SELECT_CONTATO_POR_ID = "SELECT [Site],[Telefone] FROM [dbo].[Contato] WHERE [Id] = @Id";
        readonly static string SELECT_LOCALIZACAO_POR_ID = "SELECT [Bairro],[Logradouro],[Latitude],[Longitude] FROM [dbo].[Localizacao] WHERE [Id] = @Id";

        readonly static string DELETE_RESTAURANTE = "DELETE FROM [dbo].[Restaurante] WHERE [Id] = @Id";
        readonly static string DELETE_CONTATO = "DELETE FROM [dbo].[Contato] WHERE [Id] = @Id";
        readonly static string DELETE_LOCALIZACAO = "DELETE FROM [dbo].[Localizacao] WHERE [Id] = @Id";

        
        private string ConnectionString { get; set; }

        public Restaurantes(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        internal void InserirRestaurante(Restaurante restaurante)
        {
            if(restaurante.Contato != null && restaurante.Localizacao != null)
            {
                int idContato = InserirContato(restaurante.Contato);
                int idLocalizacao = InserirLocalizacao(restaurante.Localizacao);

                using (var connection = new SqlConnection(this.ConnectionString))
                {
                    using (var inserirRestaurante = new SqlCommand(INSERT_RESTAURANTE, connection))
                    {
                        inserirRestaurante.Parameters.Add("@Capacidade", SqlDbType.Int).Value = restaurante.Capacidade;
                        inserirRestaurante.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = restaurante.Nome;
                        inserirRestaurante.Parameters.Add("@Categoria", SqlDbType.NVarChar).Value = restaurante.Categoria;
                        inserirRestaurante.Parameters.Add("@idContato", SqlDbType.NVarChar).Value = idContato;
                        inserirRestaurante.Parameters.Add("@idLocalizacao", SqlDbType.NVarChar).Value = idLocalizacao;

                        connection.Open();
                        int resultado = inserirRestaurante.ExecuteNonQuery();
                    }
                }
            }
        }

        private int InserirContato(Contato contato)
        {
            int idCriado = 0;

            using (var connection = new SqlConnection(this.ConnectionString))
            {
                using (var inserirContato = new SqlCommand(INSERT_CONTATO, connection))
                {
                    var site = contato.Site != null ? contato.Site : (Object) DBNull.Value;
                    var telefone = contato.Telefone != null ? contato.Telefone : (Object) DBNull.Value;

                    inserirContato.Parameters.Add("@Site", SqlDbType.NVarChar).Value = site;
                    inserirContato.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = telefone;

                    connection.Open();
                    //int resultado = inserirContato.ExecuteNonQuery();
                    idCriado = Convert.ToInt32(inserirContato.ExecuteScalar());
                }
            }

            return idCriado;
        }

        private int InserirLocalizacao(Localizacao localizacao)
        {
            int idCriado = 0;
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                using (var inserirLocalizacao = new SqlCommand(INSERT_LOCALIZACAO, connection))
                {
                    inserirLocalizacao.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = localizacao.Bairro;
                    inserirLocalizacao.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = localizacao.Logradouro;
                    inserirLocalizacao.Parameters.Add("@Latitude", SqlDbType.Float).Value = localizacao.Latitude;
                    inserirLocalizacao.Parameters.Add("@Longitude", SqlDbType.Float).Value = localizacao.Longitude;

                    connection.Open();
                    //int resultado = inserirLocalizacao.ExecuteNonQuery();
                    idCriado = Convert.ToInt32(inserirLocalizacao.ExecuteScalar());
                }
            }

            return idCriado;
        }

        internal IEnumerable<Contato> getContatos()
        {
            IList<Contato> contatos = new List<Contato>();

            using (var connection = new SqlConnection(this.ConnectionString))
            {
                using (var lerContatos = new SqlCommand(SELECT_CONTATO, connection))
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

        public void Remover(int id)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                var idContato = 0;
                var idLocalizacao = 0;

                using (var buscarIdContatoIdLocalizacao = new SqlCommand(SELECT_IDCONTATO_IDLOCALIZACAO, connection))
                {
                    buscarIdContatoIdLocalizacao.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    connection.Open();
                    var reader = buscarIdContatoIdLocalizacao.ExecuteReader();

                    while(reader.Read())
                    {
                        idContato = reader.GetInt32(0);
                        idLocalizacao = reader.GetInt32(1);
                    }
                }

                RemoverRestaurante(id);
                RemoverContato(idContato);
                RemoverLocalizacao(idLocalizacao);
            }
        }

        private void RemoverRestaurante(int id)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                using (var excluirRestaurante = new SqlCommand(DELETE_RESTAURANTE, connection))
                {
                    excluirRestaurante.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    connection.Open();
                    int resutlado = excluirRestaurante.ExecuteNonQuery();
                }
            }
        }
        
        private void RemoverContato(int id)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                using (var excluirContato = new SqlCommand(DELETE_CONTATO, connection))
                {
                    excluirContato.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    connection.Open();
                    int resutlado = excluirContato.ExecuteNonQuery();
                }
            }
        }

        private void RemoverLocalizacao(int id)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                using (var excluirLocalizacao = new SqlCommand(DELETE_LOCALIZACAO, connection))
                {
                    excluirLocalizacao.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                    connection.Open();
                    int resutlado = excluirLocalizacao.ExecuteNonQuery();
                }
            }
        }

        //private Contato buscarContato(int id)
        //{
        //    Contato contato = new Contato();
        //    using (var connection = new SqlConnection(this.ConnectionString))
        //    {
        //        using (var buscarContato = new SqlCommand(SELECT_CONTATO_POR_ID, connection))
        //        {
        //            buscarContato.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        //            connection.Open();
        //            var reader = buscarContato.ExecuteReader();

        //            while(reader.Read())
        //            {
        //                contato.Site = reader.GetString(0) != (Object) DBNull.Value ? reader.GetString(0) : null;
        //                contato.Telefone = reader.GetString(1) != (Object)DBNull.Value ? reader.GetString(1) : null;
        //            }
        //        }
        //    }

        //    return contato;
        //}

        //private Localizacao buscarLocalizacao(int id)
        //{
        //    Localizacao localizacao = new Localizacao();
        //    using (var connection = new SqlConnection(this.ConnectionString))
        //    {
        //        using (var buscarLocalizacao = new SqlCommand(SELECT_LOCALIZACAO_POR_ID, connection))
        //        {
        //            buscarLocalizacao.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        //            connection.Open();
        //            var reader = buscarLocalizacao.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                localizacao.Bairro = reader.GetString(0);
        //                localizacao.Logradouro = reader.GetString(1);
        //                localizacao.Latitude = reader.GetInt32(2);
        //                localizacao.Longitude = reader.GetInt32(3);
        //            }
        //        }
        //    }

        //    return localizacao;
        //}

        public IEnumerable<Restaurante> Todos()
        {
            List<Restaurante> todos = new List<Restaurante>();
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                using (var buscarRestaurantes = new SqlCommand(SELECT_RESTAURANTES, connection))
                {
                    connection.Open();
                    var reader = buscarRestaurantes.ExecuteReader();

                    while (reader.Read())
                    {
                        todos.Add(
                            new Restaurante
                            {
                                Capacidade = reader.GetInt32(0),
                                Nome = reader.GetString(1),
                                Categoria = (Categoria)Enum.Parse(typeof(Categoria), reader.GetString(2), ignoreCase: true),

                                Contato = new Contato
                                {
                                    Site = reader.IsDBNull(3) ? null : reader.GetString(3),
                                    Telefone = reader.IsDBNull(4) ? null : reader.GetString(4)
                                },

                                Localizacao = new Localizacao
                                {
                                    Bairro = reader.GetString(5),
                                    Logradouro = reader.GetString(6),
                                    Latitude = reader.GetDouble(7),
                                    Longitude = reader.GetDouble(8)
                                }
                            });
                    }
                }
            }

            return todos;
        }
    }
}
