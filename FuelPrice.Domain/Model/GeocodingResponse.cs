using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelPrice.Domain.Model
{
    public class GeocodingResponse
    {
        public List<Address> Results { get; set; }
    }

    public class Address
    {
        public Geometry Geometry { get; set; }
        public string formatted_address { get; set; }
    }

    public class Geometry
    {
        public Location Location { get; set; }
    }

    public class Location {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}
