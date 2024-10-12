using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Homework7_Quiz_
{
    public class TestInformation
    {
        public async Task<string> GetTestData()
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = "https://education.turgaliev.kz/imran/tests/\r\n";
                string responseData = await client.GetStringAsync(url);
                return responseData;
            }
            catch(HttpRequestException e)
            {
                Program.ShowText("Ошибка подключения!", true, true);
                Program.ShowText($"Message : {e.Message}");
                return null;
            }
        }
    }
}
