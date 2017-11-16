using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CalcClientAsync
{
    public class Client
    {
        private HttpClient client;
        private string uri;

        public Client(string servAddress)
        {
            client = new HttpClient();
            uri = servAddress;
        }

        public async Task<int> RequestCalcResult(int a, int b, char op)
        {
            var temp = uri + "?a=" + a + "&b=" + b + "&op=" + op;
            string response = await client.GetStringAsync(temp);
            return Convert.ToInt32(response);
        }
    }
}
