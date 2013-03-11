using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using HtmlAgilityPack;

namespace CVE_BID_tool
{
    class CVE
    {
        private string id;
        private string description;
        public List<BID> bids;

        public CVE(string cve)
        {
            this.bids = new List<BID>();
            if (verifyCVE(cve))
            {
                this.id = cve.Trim();
                this.description = setDescription();
                curl();
            }
        }

        private static Boolean verifyCVE(string cve)
        {
            Regex valid_cve = new Regex(@"CVE-[0-9]{4}-[0-9]{4}");
            Match is_valid = valid_cve.Match(cve);
            return is_valid.Success;
        }

        private string setDescription()
        {
            var cli = new WebClient();
            string address = "https://cve.mitre.org/cgi-bin/cvename.cgi?name=" + this.id;
            string data = cli.DownloadString(address);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(data);
            HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@id='GeneratedTable']").SelectSingleNode(".//table");
            HtmlNodeCollection t = node.SelectNodes(".//tr");
            string descrip =  t[3].InnerText.Replace("\r\n\r\n", "").Trim();
            return (Regex.Replace(descrip, @"\s+", " "));
        }

        private void curl()
        {
            Regex bid = new Regex(@"/bid/([0-9]+)");
            string URI = "http://www.securityfocus.com/bid";
            string myParameters = "op=display_list&c=12&vendor=&title=&version=&CVE=" + this.id;
            WebClient wc = new WebClient();
            wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            string HtmlResult = wc.UploadString(URI, myParameters);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HtmlResult);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectSingleNode("//div[@style='padding: 4px;']").SelectNodes(".//a");
            if (nodes != null)
            {
                foreach (HtmlNode n in nodes)
                {
                    Match m = bid.Match(n.Attributes["href"].Value);
                    if (m.Success && n.InnerText.Substring(0, 4) != "http")
                    {
                        BID tmp = new BID(m.Groups[1].Captures[0].Value, n.InnerText);
                        this.bids.Add(tmp);
                    }
                }
            }
        }

        public string getID()
        {
            return this.id;
        }

        public string getDescription()
        {
            return this.description;
        }

        public string getBIDs()
        {
            return null;
        }
    }
}
