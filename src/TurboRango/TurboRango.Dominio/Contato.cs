using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    public class Contato : Entidade
    {

        public string Site { get; set; }
        public string Telefone { get; set; }

        public Contato() { }

        public Contato(string site, string telefone)
        {
            this.Site = site;
            this.Telefone = telefone;
        }

        
    }
}
