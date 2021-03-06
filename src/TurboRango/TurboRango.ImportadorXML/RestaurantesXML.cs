﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TurboRango.Dominio;

namespace TurboRango.ImportadorXML
{
    public class RestaurantesXML
    {
        public string NomeArquivo { get; private set; }

        IEnumerable<XElement> restaurantes;

        /// <summary>
        /// Constrói RestaurantesXML a partir de um nome de arquivo.
        /// </summary>
        /// <param name="nomeArquivo">Nome do arquivo XML a ser manipulado</param>
        public RestaurantesXML(string nomeArquivo)
        {
            NomeArquivo = nomeArquivo;
            restaurantes = XDocument.Load(NomeArquivo).Descendants("restaurante");
        }

        public IList<string> ObterNomes()
        {
            //var resultado = new List<string>();

            //var nodos = restaurantes;

            //foreach (var item in nodos)
            //{
            //    resultado.Add(item.Attribute("nome").Value);
            //}

            //return resultado;

            /*var res = restaurantes
                .Select(n => new Restaurante
                {
                    Nome = n.Attribute("nome").Value,
                    Capacidade = Convert.ToInt32(n.Attribute("capacidade").Value)
                });
            return res.Where(x => x.Capacidade < 100).Select(x => x.Nome).OrderBy(x => x).ToList();
            */

            return (
                from n in restaurantes
                orderby n.Attribute("nome").Value descending
                where Convert.ToInt32(n.Attribute("capacidade").Value) < 100
                select n.Attribute("nome").Value
            ).ToList();
        }

        public double CapacidadeMedia()
        {
            return (
                from n in restaurantes
                select Convert.ToInt32(n.Attribute("capacidade").Value)
            ).Average();
        }

        public double CapacidadeMaxima()
        {
            var mad = (
                from n in restaurantes
                select Convert.ToInt32(n.Attribute("capacidade").Value)
            );

            return mad.Max();
        }

        //public IList<Restaurante> AgruparPorCategoria()
        //{
        //    var res = (from n in restaurantes
        //              group n by n.Attribute("categoria").Value into g
        //              select new
        //              {
        //                  Categoria = g.Key,
        //                  Restaurantes = g.ToList(),
        //                  SomatorioCapacidades = g.Sum(x => Convert.ToInt32(x.Attribute("capacidade").Value))
        //              });


        //    //throw new NotImplementedException();
        //}

        public IList<string> OrdenarPorNomeAsc()
        {
            return (
                from x in restaurantes
                orderby x.Attribute("nome").Value ascending
                select x.Attribute("nome").Value
                ).ToList();
        }

        //public IList<string> ObterSites()
        //{
        //    return (
        //        from x in restaurantes
        //        where x.HasElements("")
        //        select x.Element("site").Value
        //        ).ToList();
        //}


        //exercicio 2

        public IEnumerable<Restaurante> TodosRestaurantes()
        {
            return (
                from n in restaurantes
                let contato = n.Element("contato")
                let site = contato != null && contato.Element("site") != null ? contato.Element("site").Value : null
                let telefone = contato != null && contato.Element("telefone") != null ? contato.Element("telefone").Value : null
                let localizacao = n.Element("localizacao")
                select new Restaurante
                {
                    Nome = n.Attribute("nome").Value,
                    Capacidade = Convert.ToInt32(n.Attribute("capacidade").Value),
                    Categoria = (Categoria)Enum.Parse(typeof(Categoria), n.Attribute("categoria").Value, ignoreCase: true),
                    Contato = new Contato
                    {
                        Site = site,
                        Telefone = telefone
                    },
                    Localizacao = new Localizacao
                    {
                        Bairro = localizacao.Element("bairro").Value,
                        Logradouro = localizacao.Element("logradouro").Value,
                        Latitude = Convert.ToDouble(localizacao.Element("latitude").Value),
                        Longitude = Convert.ToDouble(localizacao.Element("longitude").Value)
                    }
                }
            );
        }
    }
}