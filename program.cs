using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
namespace ICONrandom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Image image = new RandomIcon().Random();
        }
        class RandomIcon {
            private WebClient client = new WebClient();
            public Image Random() {
                List<string> icon = new List<string>();
                char[] lettre = "AZERTYUIOPQSDFGHJKLMWXCVBN?1234567890azertyuiopqsdfghjklmwxcvbn".ToArray();
                string req = client.DownloadString("https://www.iconfinder.com/search/?q=" + lettre[new Random().Next(0, lettre.Length)] + "&price=free&style=ico");
                MatchCollection matches = Regex.Matches(req, "href=\"/icons/([0-9]+)", RegexOptions.Multiline);
                foreach (Match match in matches)
                    foreach (Capture capture in match.Captures)
                        icon.Add(capture.Value);
                return Bitmap.FromStream(new MemoryStream(client.DownloadData($"https://www.iconfinder.com{icon[new Random().Next(0, icon.Count - 1)].Replace("href=\"", "")}/download/ico")));
            }
        }
    }
}
