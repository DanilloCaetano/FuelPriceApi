using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelPrice.Domain.Model
{
    public class FuelPriceMunResponse
    {
        public FuelPriceMunResponse() { }

        public FuelPriceMunResponse(FuelPriceMunResponse resp)
        {
            Name = resp.Name;
            Address = resp.Address;
            Neighborhood = resp.Neighborhood;
            Brand = resp.Brand;
            SalePrice = resp.SalePrice;
            Lat = resp.Lat;
            Lng = resp.Lng;
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Neighborhood { get; set; }
        public string Brand { get; set; }
        public string SalePrice { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
