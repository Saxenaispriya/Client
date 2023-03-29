using Microsoft.AspNetCore.Mvc;
using ConsumeWithNewAPI.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using MessagePack.Formatters;

namespace ConsumeWithNewAPI.Controllers
{
    public class Weatherinfo : Controller
    {

        //[HttpGet("{lon}/{lat}")]
        //[HttpPost]
        

        public async Task<int[]> GetinfoApi(double longitude,double latitude)
        {
            //    var model = new Root
            //    {
            //        main = new Main
            //        {
            //            temp = 0
            //}
            //    };
            int[] intArray = { 1, 2, 3 };

            for (int i=0;i<3;i++)
            {
                var endpoint = "https://localhost:7215/api/WeatherInfo";
                var urldata = string.Format("{0}?lat={1}&lon={2}", endpoint, longitude, latitude);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, urldata);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var body = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<Root>(body,
                new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                intArray[i] = (int)result.main.temp;
                longitude= longitude+1;
                latitude= latitude+1;

                //model.main.temp = result.main.temp;
            }
            return intArray;
           // return View(intArray);
        }
    }
}
