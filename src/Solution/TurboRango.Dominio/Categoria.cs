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
        [Description("Comun")] Comun,
        [Description("Cozinha Natural")] CozinhaNatural,
        [Description("Cozinha Mexicana")] CozinhaMexicana,
        [Description("Churrascaria")] Churrascaria,
        [Description("Cozinha Japonesa")] CozinhaJaponeja,
        [Description("Fastfood")] Fastfood,
        [Description("Pizzaria")] Pizzaria
    }
}
