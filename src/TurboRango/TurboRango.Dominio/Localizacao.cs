using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    public class Localizacao
    {
        public string Bairro { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Logradouro { get; set; }

        public Localizacao() { }
        public Localizacao(string bairro, double latitude, double longitude, string logradouro)
        {
            this.Bairro = bairro;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Logradouro = logradouro;
        }
    }
}
