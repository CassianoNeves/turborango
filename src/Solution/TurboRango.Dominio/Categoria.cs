using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurboRango.Dominio
{
    enum Categoria
    {
        [Description("Comun")] COMUN,
        [Description("Cozinha Natural")] COZINHA_NATURAL,
        [Description("Cozinha Mexicana")] COZINHA_MEXICANA,
        [Description("Churrascaria")] CHURRASCARIA,
        [Description("Cozinha Japonesa")] COZINHA_JAPONESA,
        [Description("Fastfood")] FASTFOOD,
        [Description("Pizzaria")] PIZZARIA
    }
}
