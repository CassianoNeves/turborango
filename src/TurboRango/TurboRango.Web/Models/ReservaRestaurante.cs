using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TurboRango.Dominio;

namespace TurboRango.Web.Models
{
    public class ReservaRestaurante
    {
        public Reserva Reserva { get; set; }
        public Restaurante Restaurante { get; set; }
    }
}