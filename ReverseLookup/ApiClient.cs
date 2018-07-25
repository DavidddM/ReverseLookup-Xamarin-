using System;

using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ReverseLookup
{
    class APIClient
    {
        public Uri ApiUri { get; private set; }
        public Task<byte[]> Response { get; private set; }

        public APIClient(Uri ApiUri)
        {
            this.ApiUri = ApiUri;
        }

        public void AsyncGet()
        {
            using (WebClient wc = new WebClient())
            {
                Response = wc.DownloadDataTaskAsync(ApiUri);
            }
        }

        public void AsyncPost(NameValueCollection data)
        {
            using (WebClient wc = new WebClient())
            {
                Response = wc.UploadValuesTaskAsync(ApiUri, data);
            }
        }

        public void AsyncPost(string jsonString)
        {
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
            NameValueCollection data = new NameValueCollection(dict.Count);
            foreach (var d in dict)
            {
                data.Add(d.Key, d.Value);
            }
            AsyncPost(data);
        }

        public void AsyncPost(Dictionary<string, string> dict)
        {
            NameValueCollection data = new NameValueCollection(dict.Count);
            foreach (var d in dict)
            {
                data.Add(d.Key, d.Value);
            }
            AsyncPost(data);
        }
    }
}