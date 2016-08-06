using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Net;

namespace PushToPlay.Core.PlatformFactory
{
    public class UrlBuilder
    {
        private string _authority;
        private string _host;
        private int? _port;
        private Dictionary<string, object> _query = new Dictionary<string, object>();

        public UrlBuilder(string host)
            : this("http", host, null)
        {

        }
        public UrlBuilder(string authority, string host)
            : this(authority, host, null)
        {
        }
        public UrlBuilder(string authority, string host, int? port)
        {
            this._authority = authority;
            this._host = host;
            this._port = port;
        }

        public UrlBuilder AddQuery(string key, object value)
        {
            this._query.Add(key, value);
            return this;
        }

        public override string ToString()
        {
            string url = _authority + "://" + _host;
            if (_port.HasValue)
            {
                url += ":" + _port.ToString();
            }


            return AppendQuery(url);
        }

        private string AppendQuery(string url)
        {
            if (_query.Count == 0)
            {
                return url;
            }

            url += "?";
            bool isNotFirst = false;
            foreach (var key in this._query.Keys)
            {
                if (isNotFirst)
                {
                    url += "&";
                }
                url += HttpUtility.UrlEncode(key) + "=" + HttpUtility.UrlEncode(this._query[key].ToString());
                isNotFirst = true;
            }

            return url;
        }
    }
}
