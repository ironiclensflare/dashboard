using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Dashboard.Models;
using System.Xml;

namespace Dashboard.Models
{
    public class GDSignup
    {
        public HttpStatusCode statusCode { get; private set; }
        public string apiEndpoint = "https://stage-api.govdelivery.com/api/account/UKNOTTSCC/subscribers/add_subscriptions";

        private string authorization
        {
            get
            {
                string credentials = Logins.USERNAME + ":" + Logins.PASSWORD;
                byte[] credentialsByteArray = System.Text.Encoding.ASCII.GetBytes(credentials);
                string authorization = Convert.ToBase64String(credentialsByteArray);

                return authorization;
            }
        }

        public void SignUpUser(string email, string topic)
        {
            string xmlData = BuildXmlData(email, topic);
            HttpStatusCode code = MakeSignupWebRequest(xmlData);
            this.statusCode = code;
        }

        private string BuildXmlData(string email, string topic)
        {
            XmlDocument d = new XmlDocument();

            using (XmlWriter w = d.CreateNavigator().AppendChild())
            {
                w.WriteStartDocument();
                w.WriteStartElement("subscriber");
                w.WriteStartElement("email");
                w.WriteString(email);
                w.WriteEndElement();

                w.WriteStartElement("send-notifications");
                w.WriteAttributeString("type", "boolean");
                w.WriteString("false");
                w.WriteEndElement();

                w.WriteStartElement("topics");
                w.WriteAttributeString("type", "array");
                w.WriteStartElement("topic");
                w.WriteStartElement("code");
                w.WriteString(topic);
                w.WriteEndElement();
                w.WriteEndElement();
                w.WriteEndElement();
                w.WriteEndElement();
                w.WriteEndDocument();
            }

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = System.Text.Encoding.UTF8;

            using (var s = new Utf8StringWriter())
            using (var x = XmlWriter.Create(s))
            {
                d.WriteTo(x);
                x.Flush();

                return s.GetStringBuilder().ToString();
            }
        }

        private HttpStatusCode MakeSignupWebRequest(string xmlData)
        {
            byte[] xmlByteArray = System.Text.Encoding.ASCII.GetBytes(xmlData);

            WebRequest request = WebRequest.Create(apiEndpoint);

            request.Headers.Add("Authorization", "Basic " + this.authorization);
            request.Method = "POST";
            request.ContentLength = xmlByteArray.Length;
            request.ContentType = "application/xml";

            System.IO.Stream dataStream = request.GetRequestStream();
            dataStream.Write(xmlByteArray, 0, xmlByteArray.Length);
            dataStream.Close();

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return response.StatusCode;
            }
            catch (WebException)
            {
                return HttpStatusCode.BadRequest;
            }
        }
    }

    public class Utf8StringWriter : System.IO.StringWriter
    {
        public override System.Text.Encoding Encoding
        {
            get
            {
                return System.Text.Encoding.UTF8;
            }
        }
    }
}