using Eticaret.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Newtonsoft.Json;
using RestSharp;

namespace Eticaret.Controllers
{
    public class CollectApiController : Controller
    {
        public IActionResult Index()
        {
          
            return View();
        }

       

        public List<GoldItem> GetGoldPrice()
        {
            // collect apikey 0zPqYGhPQdBUDBLrY2W22K: 7rCU80IqhJwA3ny7THss6n
            //var client = new RestClient("https://api.collectapi.com/economy/currencyToAll?int=10&base=USD");
            var client = new RestClient("https://api.collectapi.com/economy/goldPrice");
            var request = new RestRequest("https://api.collectapi.com/economy/goldPrice", RestSharp.Method.Get);
            request.AddHeader("authorization", "apikey 0zPqYGhPQdBUDBLrY2W22K:7rCU80IqhJwA3ny7THss6n");
            request.AddHeader("content-type", "application/json");
            RestResponse response = client.Execute(request);


            string jsonData = response.Content; // Response içerisindeki veriyi jsonData değişkenine aldık

            // Deserialize JSON to GoldData object
            GoldData goldData = JsonConvert.DeserializeObject<GoldData>(jsonData);

            List<GoldItem> veriler= new List<GoldItem>();

            // Access the data
            if (goldData.Success)
            {
                foreach (var item in goldData.Result)
                {
                    //Console.WriteLine($"Name: {item.Name}, Buying: {item.Buying}, Selling: {item.Selling}, Date: {item.Date}, Time: {item.Time}");
                    veriler.Add(new GoldItem() {Name=item.Name, Buying=item.Buying, Selling=item.Selling, Date=item.Date, Time=item.Time });
                }
                return veriler;
            }
            else
            {
                return veriler;
            }
        }
    }
}
