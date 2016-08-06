using Newtonsoft.Json;
using PushToPlay.Model;
using PushToPlay.WEB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PushToPlay.WEB.Controllers.Messenger
{
    public class MessengerController : Controller
    {
        [HttpPost]
        public ActionResult PostMessage()
        {
            Stream _requestStream = Request.InputStream;

            _requestStream.Seek(0, SeekOrigin.Begin);

            string json = new StreamReader(_requestStream).ReadToEnd();

            var _message = JsonConvert.DeserializeObject<MessageJson>(json);

            if (_message != null)
            {
                _message.Initialize();

                if (_message.Delete)
                {
                    _message.DeleteMessage();
                }
                else
                {
                    _message.PostMessage();
                }
            }
           
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}
