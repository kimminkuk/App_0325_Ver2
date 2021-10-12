using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace App2_0325.Data
{
    public class DB_Manager
    {
        static readonly string BaseAddress = "temp";
        static readonly string Url = $"{BaseAddress}";
        
        private async Task<HttpClient> GetClient()
        {
            HttpClient client = new HttpClient();
            return client;
        }

        public async Task<IEnumerable<DB_VER_1>> GetAll_Ver1()
        {
            HttpClient client = await GetClient();
            string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<DB_VER_1>>(result);
        }

        public async Task<List<DB_VER_1>> Add_Ver1(int CODE, string NAME)
        {
            //TODO: POST to add a Quant Ver1
            DB_VER_1 DB_Ver1 = new DB_VER_1()
            {
                Ver1_CODE = CODE,
                Ver1_NAME = NAME
            };

            string BaseAddress_AddApi = "add";
            string UrlAdd = $"{BaseAddress_AddApi}";
            HttpClient client = await GetClient();
            var response = await client.PostAsync(UrlAdd,
                new StringContent
                (
                    JsonConvert.SerializeObject(DB_Ver1),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            return JsonConvert.DeserializeObject<List<DB_VER_1>>
            (
                await response.Content.ReadAsStringAsync()
            );
        }
    }
}