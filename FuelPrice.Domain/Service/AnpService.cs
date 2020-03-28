using FuelPrice.Domain.Interface;
using FuelPrice.Domain.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FuelPrice.Domain.Service
{
    public class AnpService : IAnpService
    {        
        private readonly string urlPerState = "http://preco.anp.gov.br/include/Resumo_Por_Estado_Municipio.asp";
        private readonly string urlPerGasStation = "http://preco.anp.gov.br/include/Resumo_Semanal_Posto.asp";
        private readonly string apiKey = "AIzaSyDWz7SeZPPdn97IKt2R2SstlZ9Vgmwai_Q";

        public async Task<List<FuelPriceResponse>> GetHtmlTableResultFromState(SearchProps searchProps)
        {            
            var values = KeyValueSearchProps(searchProps);
            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(values))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    var url = "http://preco.anp.gov.br/include/Resumo_Por_Estado_Index.asp";
                    HttpResponseMessage response = await httpClient.PostAsync(url, content);
                    httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                    HttpResponseMessage responseGet = await httpClient.GetAsync(urlPerState);

                    var stringHtml = await responseGet.Content.ReadAsStringAsync();
                    var doc = new HtmlDocument();
                    doc.LoadHtml(stringHtml);

                    var tables = doc.DocumentNode.SelectNodes("//table/tr/td");
                    var i = -1;

                    var result = new List<FuelPriceResponse>();
                    var fuelPrice = new FuelPriceResponse();

                    foreach (var td in tables)
                    {
                        if (td.Attributes["class"]?.Value != "subtitle")
                        {
                            i++;

                            if (i <= 6) fuelPrice.GetType()?.GetProperties()?[i].SetValue(fuelPrice, td.InnerText);                            
                        }

                        if (i == 10)
                        {
                            var obj = new FuelPriceResponse(fuelPrice);
                            result.Add(obj);
                            i = -1;
                        }
                    }

                    return result;
                }
            }
        }

        public async Task<List<FuelPriceMunResponse>> GetHtmlTableResultFromGasStation(SearchPropMun searchProps, bool latLong = false)
        {
            var values = KeyValueSearchProps(searchProps);
            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(values))
                {                    
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    var url = "http://preco.anp.gov.br/include/Resumo_Por_Estado_Index.asp";
                    HttpResponseMessage response = await httpClient.PostAsync(url, content);
                    httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                    HttpResponseMessage responseGet = await httpClient.PostAsync(urlPerGasStation, content);

                    var stringHtml = await responseGet.Content.ReadAsStringAsync();
                    var doc = new HtmlDocument();
                    doc.LoadHtml(stringHtml);

                    var tables = doc.DocumentNode.SelectNodes("//html/body/div/div/div/div/div/form/span/div/table/tr/td");
                    var i = -1;

                    var result = new List<FuelPriceMunResponse>();
                    var fuelPrice = new FuelPriceMunResponse();

                    foreach (var td in tables)
                    {                        
                        if (td.Attributes["class"]?.Value != "subtitle")
                        {
                            i++;

                            if (i <= 4) fuelPrice.GetType()?.GetProperties()?[i].SetValue(fuelPrice, td.InnerText);
                        }

                        if (i == 8)
                        {
                            var obj = new FuelPriceMunResponse(fuelPrice);
                            result.Add(obj);
                            i = -1;
                        }
                    }

                    var finalResult = new List<FuelPriceMunResponse>();

                    if(latLong)
                    {
                        foreach(var r in result)
                        {
                            try
                            {
                                var urlGeoApi = $"https://maps.googleapis.com/maps/api/geocode/json?address={r.Address}&key=AIzaSyDWz7SeZPPdn97IKt2R2SstlZ9Vgmwai_Q";
                                HttpResponseMessage responseGetGeo = await httpClient.GetAsync(urlGeoApi);
                                var json = await responseGetGeo.Content.ReadAsStringAsync();
                                var geoconding = JsonConvert.DeserializeObject<GeocodingResponse>(json);

                                r.Lat = Convert.ToDouble(geoconding?.Results?.FirstOrDefault().Geometry.Location.lat ?? 0);
                                r.Lng = Convert.ToDouble(geoconding?.Results?.FirstOrDefault().Geometry.Location.lng ?? 0);
                                r.SalePrice = r.SalePrice.Replace(',', '.');

                                finalResult.Add(r);
                            }
                            catch
                            {
                                continue;
                            }
                        }                        
                    }

                    return finalResult;
                }
            }
        }

        private async Task PrepareObjectAuth(HttpClient httpClient, FormUrlEncodedContent content)
        {
            var url = "http://preco.anp.gov.br/include/Resumo_Por_Estado_Index.asp";            
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
        }

        private IEnumerable<KeyValuePair<string, string>> KeyValueSearchProps(SearchProps searchProps)
        {
            Type propsClassType = searchProps.GetType();
            PropertyInfo[] properties = propsClassType.GetProperties();
            var result = new List<KeyValuePair<string, string>>();

            foreach (PropertyInfo pi in properties)
            {
                var keyValue = new KeyValuePair<string, string>(pi.Name, pi.GetValue(searchProps, null)?.ToString() ?? "");
                result.Add(keyValue);
            }

            return result;
        }

        private IEnumerable<KeyValuePair<string, string>> KeyValueSearchProps(SearchPropMun searchProps)
        {
            Type propsClassType = searchProps.GetType();
            PropertyInfo[] properties = propsClassType.GetProperties();
            var result = new List<KeyValuePair<string, string>>();

            foreach (PropertyInfo pi in properties)
            {
                var keyValue = new KeyValuePair<string, string>(pi.Name, pi.GetValue(searchProps, null)?.ToString() ?? "");
                result.Add(keyValue);
            }

            return result;
        }
    }
}
