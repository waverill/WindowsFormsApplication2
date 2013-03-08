using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace CVE_BID_tool
{
    class BID
    {
        public string id;
        public string description;

        public BID(string bid)
        {
            this.id = bid;
            this.description = getDescription();
        }

        public BID(string bid, string desc)
        {
            this.id = bid;
            this.description = desc;
        }

        private string getDescription()
        {
            var cli = new WebClient();
            string address = "http://www.securityfocus.com/bid/" + this.id.Trim();
            string data = cli.DownloadString(address);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(data);
            HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@id='vulnerability']").SelectSingleNode(".//span[@class='title']");

            //HtmlNodeCollection t = node.SelectNodes(".//tr");
           // string descrip = t[3].InnerText.Replace("\r\n\r\n", "").Trim();
            return (node.InnerText.Trim());
        }
    }
}
