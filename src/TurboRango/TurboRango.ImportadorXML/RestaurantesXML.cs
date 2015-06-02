using System;
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

        /// <summary>
        /// Contrói  Restaurentes apartir de um nome de arquivo.
        /// </summary>
        /// <param name="nomeArquivo"></param>
        public RestaurantesXML( string nomeArquivo )
        {
            this.NomeArquivo = nomeArquivo;
        }

        public IList<string> ObterNomes()
        {
            IEnumerable<XElement> restaurantes;
            //var resultado = new List<string>();

            //var nodos = XDocument.Load(NomeArquivo).Descendants("restaurante");
            
            //foreach (var item in nodos)
            //{
            //    resultado.Add(item.Attribute("nome").Value);
            //}

            //return resultado;

            //return XDocument
            //    .Load(NomeArquivo)
            //    .Descendants("restaurante")
            //    .Select(n => n.Attribute("nome").Value)
            //    .ToList();

            //return (
            //    from n in XDocument.Load(NomeArquivo).Descendants("restaurante")
            //    orderby n.Attribute("nome").Value
            //    select n.Attribute("nome").Value
            //).ToList();

            var res = restaurantes
                .Select(n => new Restaurante
                {
                    Nome = n.Attribute("nome").Value,
                    Capacidade = Convert.ToInt32(n.Attribute("capacidade").Value)
                });

            return res.Where(x => x.Capacidade > 100).Select(x => x.Nome).OrderBy(x => x);
        }
    }
}
