using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelPrice.Domain.Model
{
    public class SearchProps
    {
        public string selSemana { get; set; }
        public string desc_Semana { get; set; }
        public string cod_Semana { get; set; }
        public string tipo { get; set; } = "1";
        public string selEstado { get; set; }
        public string Cod_Combustivel { get; set; }
        public string selCombustivel { get; set; } = "487*Gasolina";
        public string txtValor { get; set; } = "HMLJ";
        public string image1 { get; set; }
    }
}
