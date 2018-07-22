using System;

using System.Collections.Specialized;
using System.Net;
using System.Threading.Tasks;

namespace ReverseLookup
{
    class APIClient
    {
        public Uri ApiKey { get; private set; }
        public Task<byte[]> Response { get; private set; }

        public APIClient(Uri ApiKey)
        {
            this.ApiKey = ApiKey;
        }

        public void AsyncGet()
        {
            using (WebClient wc = new WebClient())
            {
                Response = wc.DownloadDataTaskAsync(ApiKey);
            }
        }

        public void AsyncPost(NameValueCollection data)
        {
            using (WebClient wc = new WebClient())
            {
                Response = wc.UploadValuesTaskAsync(ApiKey, data);
            }
        }
    }
}