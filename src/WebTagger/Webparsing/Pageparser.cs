using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebTagger.Jobs;

namespace WebTagger.Webparsing
{
    public class Pageparser
    {
        private HtmlDocument doc;

        public Pageparser(string pageHtml)
        {
            doc = new HtmlDocument();
            doc.LoadHtml(pageHtml);
        }

        public List<string> GetSelectionValues(string searchpath)
        {
            var retVal = new List<string>();

            string valueExpression = "";
            var selectorParts = searchpath.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if(selectorParts.Last().StartsWith("r:") || selectorParts.Last().StartsWith("a:"))
            {
                valueExpression = selectorParts.Last();
                searchpath = string.Join(" ", selectorParts.Take(selectorParts.Length - 1));
            }

            var nodes = doc.QuerySelectorAll(searchpath);
            foreach(var node in nodes)
            {
                if(valueExpression.StartsWith("r:"))
                {
                    var pattern = valueExpression.Substring(2).Replace("&nbsp;", " ");
                    var regex = new Regex(pattern);
                    foreach(Match match in regex.Matches(node.InnerText.Trim()))
                    {
                        if(match.Groups.Count > 1)
                        {
                            retVal.Add(match.Groups[match.Groups.Count - 1].Value);
                        }
                        else
                        {
                            retVal.Add(match.Value);
                        }
                        
                    }
                }
                else if (valueExpression.StartsWith("a:"))
                {
                    var attrValue = node.GetAttributeValue(valueExpression.Substring(2), "");
                    retVal.Add(attrValue.Trim());
                }
                else
                {
                    retVal.Add(node.InnerText.Trim());
                }
            }

            return retVal;
        }
    }
}
