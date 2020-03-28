using FuelPrice.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelPrice.Domain.Interface
{
    public interface IAnpService
    {        
        Task<List<FuelPriceResponse>> GetHtmlTableResultFromState(SearchProps searchProps);        
        Task<List<FuelPriceMunResponse>> GetHtmlTableResultFromGasStation(SearchPropMun searchProps, bool latlng = false);        
    }
}
