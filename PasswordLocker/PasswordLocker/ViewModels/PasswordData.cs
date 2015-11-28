using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;


namespace PasswordLocker.ViewModels
{
    public class PasswordData
    {
         [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }


        [JsonProperty(PropertyName="title")]
        public string Title { get; set; }


        [JsonProperty(PropertyName = "user_id")]
        public string User_Id { get; set; }


        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }


        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }


        [JsonProperty(PropertyName = "phone_no")]
        public string Phone_no { get; set; }

        [JsonProperty(PropertyName = "other_info")]
        public string OtherInfo { get; set; }

       
    }

}
