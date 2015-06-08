using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Exemplos

            Restaurante restaurante = new Restaurante();

            //restaurante.
            //Console.Write(restaurante.Contato.Site);
            Console.WriteLine(
                restaurante.Capacidade.HasValue ?
                    restaurante.Capacidade.Value.ToString() :
                    "oi"
                );

            restaurante.Nome = string.Empty + " ";

            Console.WriteLine(restaurante.Nome ?? "Nulo!!!");

            Console.WriteLine(!string.IsNullOrEmpty(restaurante.Nome.Trim()) ? "tem valor" : "não tem valor");

            var oQueEuGosto = "bacon";

            var texto = String.Format("Eu gosto de {0}", oQueEuGosto);
            // var texto = String.Format("Eu gosto de \{oQueEuGosto}");

            StringBuilder pedreiro = new StringBuilder();
            pedreiro.AppendFormat("Eu gosto de {0}", oQueEuGosto);
            pedreiro.Append("!!!!!!");

            object obj = "1";
            int a = Convert.ToInt32(obj);
            int convertido = 10;
            bool conseguiu = Int32.TryParse("1gdfgfd", out convertido);
            int res = 12 + a;

            Console.WriteLine(pedreiro);

            #endregion

            const string nomeArquivo = "restaurantes.xml";

            var restaurantesXML = new RestaurantesXML(nomeArquivo);
            var nomes = restaurantesXML.ObterNomes();
            var capacidadeMedia = restaurantesXML.CapacidadeMedia();
            var capacidadeMaxima = restaurantesXML.CapacidadeMaxima();
            //var porCategoria = restaurantesXML.AgruparPorCategoria();
            var restaurantesOrdenadosPorNomesAcendente = restaurantesXML.OrdenarPorNomeAsc();
            //var restaurantesComSite = restaurantesXML.ObterSites();


            #region ADO.NET

            var connString = @"Data Source=.\CASSIANO;Initial Catalog=TurboRango_dev;Integrated Security=True;";

            var restaurantes = new Restaurantes(connString);

            //acessoAoBanco.InserirContato(new Contato
            //{
            //    Site = "www.dogão.com",
            //    Telefone = "55555"
            //});

            //acessoAoBanco.InserirLocalizacao(new Localizacao 
            //{
            //    Logradouro = "Rua Sete de Setembro, 1045 - Liberdade",
            //    Bairro = "Liberdade",
            //    Latitude = -29.712571,
            //    Longitude = -51.13636
            //});

            //restaurantes.InserirRestaurante(new Restaurante
            //{
            //    Capacidade = 100,
            //    Nome = "GARFÃO RESTAURANTE E PIZZARIA",
            //    Localizacao = new Localizacao
            //    {
            //        Logradouro = "Rua Sete de Setembro, 1045 - Liberdade",
            //        Bairro = "Liberdade",
            //        Latitude = -29.712571,
            //        Longitude = -51.13636
            //    },
            //    Contato = new Contato
            //    {
            //        Site = "www.dogão.com",
            //        Telefone = "55555"
            //    },
            //    Categoria = Categoria.Comum
            //});

            //IEnumerable<Contato> contatos = acessoAoBanco.getContatos();

            //#region InserirTotosRestaurantesDoXML

            //var todosRestaurantes = restaurantesXML.TodosRestaurantes();

            //foreach (var restauranteAtual in todosRestaurantes)
            //{
            //    restaurantes.InserirRestaurante(restauranteAtual);
            //}

           // #endregion

            //#region ExcluirRestaurante

            //restaurantes.Remover(1);

            //#endregion

            //var todos = restaurantes.Todos();

            restaurantes.Atualizar(2, new Restaurante
            {
                Capacidade = 100,
                Nome = "GARFÃO RESTAURANTE E PIZZARIA",
                Localizacao = new Localizacao
                {
                    Logradouro = "Rua Sete de Setembro, 1045 - Liberdade",
                    Bairro = "Liberdade",
                    Latitude = -29.712571,
                    Longitude = -51.13636
                },
                Contato = new Contato
                {
                    Site = "www.dogão.com",
                    Telefone = "55555"
                },
                Categoria = Categoria.Comum
            });

            #endregion
        }
    }
}