using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace App2_0325.Data
{
    public class DB_VER_1
    {
        [JsonProperty("Ver1_ID")]
        public int Ver1_ID {get; set;}

        [JsonProperty("Ver1_CODE")]
        public int Ver1_CODE {get; set;}

        [JsonProperty("Ver1_NAME")]
        public string Ver1_NAME {get; set;}
    }
}