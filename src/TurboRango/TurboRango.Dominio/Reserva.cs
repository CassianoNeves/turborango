using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
     public class Reserva : Entidade
    {
         public string NomeCliente { get; set; }
        public int QtdPessoas { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime Data { get; set; }
        public string Turno { get; set; }
        public int idRestaurante { get; set; }
    }
}
