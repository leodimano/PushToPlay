using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace PushToPlay.Core.PlatformFactory
{
    public class RequestFactory : IDisposable
    {
        public Hashtable ParamList { get; set; }

        public RequestFactory()
        {
            this.ParamList = new Hashtable();
        }

        public bool ExecuteRequest(string url_, out string response_, bool SSL = false)
        {
            response_ = string.Empty;
            UrlBuilder _urlBuilder = null;

            if (!SSL)
                _urlBuilder = new UrlBuilder(url_);
            else
                _urlBuilder = new UrlBuilder("https", url_);

            foreach (DictionaryEntry _entry in this.ParamList)
            {
                _urlBuilder.AddQuery((string)_entry.Key, _entry.Value.ToString());
            }

            var _request = (HttpWebRequest)WebRequest.Create(_urlBuilder.ToString());

            try
            {
                using (var _response = (HttpWebResponse)_request.GetResponse())
                {
                    if (_request.HaveResponse && _response != null)
                    {
                        using (var _reader = new StreamReader(_response.GetResponseStream()))
                        {
                            response_ = _reader.ReadToEnd();
                            return true;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (var _errorMessage = (HttpWebResponse)ex.Response)
                    {
                        using (var _reader = new System.IO.StreamReader(ex.Response.GetResponseStream()))
                        {
                            response_ = _reader.ReadToEnd();
                            return false;
                        }
                    }
                }
                else
                {
                    return this.ExecuteRequest(url_, out response_);
                }
            }

            return false;
        }

        public void Dispose()
        {
            ParamList = null;
        }
    }

}
