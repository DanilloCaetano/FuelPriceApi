using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelPrice.Domain.Model
{
    public class FuelPriceResponse
    {
        public FuelPriceResponse() { }

        public FuelPriceResponse(FuelPriceResponse resp)
        {
            County = resp.County;
            NumStations = resp.NumStations;
            AveragePrice = resp.AveragePrice;
            Deviation = resp.Deviation;
            MinPrice = resp.MinPrice;
            MaxPrice = resp.MaxPrice;
            MarginMed = resp.MarginMed;
        }

        public string County { get; set; }        
        public string NumStations { get; set; }
        public string AveragePrice { get; set; }
        public string Deviation { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public string MarginMed { get; set; }
    }
}
