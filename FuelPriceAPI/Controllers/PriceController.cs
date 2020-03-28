using FuelPrice.Domain.Interface;
using FuelPrice.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FuelPriceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IAnpService _anpService;
        private readonly IHelpers _helpers;
        public PriceController(IAnpService anpService, IHelpers helpers)
        {
            _helpers = helpers;
            _anpService = anpService;
        }

        [HttpGet, Route("state/{state}")]
        public async Task<ActionResult<List<FuelPriceResponse>>> Get(string state = "SAO PAULO")
        {
            var searchP = new SearchProps();
            var initialDate = new DateTime(2020, 03, 14);
            var finalDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var xx = _helpers.weekDiff(initialDate, finalDate);
            var cod = 1082 + (xx);
            var codState = _helpers.searchCode(state);

            searchP.selSemana = $"{cod}*De {initialDate} a {finalDate}";
            searchP.desc_Semana = $"de {initialDate} a {finalDate}";
            searchP.selEstado = $"{codState}*{state.Replace(' ','@')}";
            searchP.cod_Semana = cod.ToString();
            var result = await _anpService.GetHtmlTableResultFromState(searchP);
           
            return result;
        }

        [HttpGet, Route("mun")]
        public async Task<ActionResult<List<FuelPriceMunResponse>>> GetMun(string mun = "SAO PAULO", bool latlng = false)
        {
            var searchP = new SearchPropMun();
            var initialDate = new DateTime(2020, 03, 14);
            var finalDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var xx = _helpers.weekDiff(initialDate, finalDate);
            var cod = 1082 + (xx);
            //var codState = _helpers.searchCode(state);

            searchP.selMunicipio = $"9668*{mun.Replace(' ', '@')}";
            searchP.cod_Semana = cod.ToString();
            searchP.desc_Semana = $"de {initialDate} a {finalDate}";
            var result = await _anpService.GetHtmlTableResultFromGasStation(searchP, latlng);

            return result;
        }
    }
}
