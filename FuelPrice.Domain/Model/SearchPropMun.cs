using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelPrice.Domain.Model
{
    public class SearchPropMun
    {        
        public string cod_Semana { get; set; }
        public string desc_Semana { get; set; }
        public string tipo { get; set; } = "1";
        public string selMunicipio { get; set; }
        public string desc_combustivel { get; set; } = "- Gasolina R$/l";
        public string cod_Combustivel { get; set; } = "487";
        public string selCombustivel { get; set; } = "487*Gasolina";
        public string txtValor { get; set; } = "HMLJ";
        public string image1 { get; set; }
    }
}
