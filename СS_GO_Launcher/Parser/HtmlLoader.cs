using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public class HtmlLoader
    {
        readonly HttpClient httpClient;

        public HtmlLoader()
        {
            httpClient = new HttpClient();
        }

        public async Task<string> GetSourceByUri(string uri)
        {
            var response = await httpClient.GetAsync(uri);

            string source = null;

            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }
    }
}
