using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace Geolocaliacao
{
    public class Geolocation
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public void LatLong(string endereco)
        {
            
            XmlDocument xml = new XmlDocument();

            string cep = endereco.Replace(".", "")
                                 .Replace("-", "");
            WebRequest request = WebRequest.Create(string.Concat("https://maps.googleapis.com/maps/api/geocode/xml?key=AIzaSyDAovaek13E489coVDnCmDdHuirkev_m5s&amp;sensor=false&address=" + cep));
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            XmlTextReader xmlReader = new XmlTextReader(response.GetResponseStream());

            xml.Load(xmlReader);

            XmlNodeList node = xml.DocumentElement.GetElementsByTagName("location");

            foreach (XmlNode xn in node)
            {
                this.Latitude = xn["lat"].InnerText;
                this.Longitude = xn["lng"].InnerText;
                this.Latitude = this.Latitude.Remove(this.Latitude.Length - 1);
                this.Longitude = this.Longitude.Remove(this.Latitude.Length - 1);
            }
        }
    }
}
