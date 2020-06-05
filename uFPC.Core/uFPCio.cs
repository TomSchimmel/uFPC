using System;
using System.Linq;
using System.Text.RegularExpressions;
using Umbraco.Core.Models;

namespace uFPC.IO
{
    public class uFPCio
    {
        public static void WriteToCache(string input, ITemplate template, DateTime lastEdited)
        {
            System.IO.File.WriteAllText(String.Format(@"{0}\App_plugins\uFPC\cache\{1}_{2}.cshtml", AppDomain.CurrentDomain.BaseDirectory, template.Alias, lastEdited.Ticks.ToString()), input);
        }

        public static string GetFromFromCache(ITemplate template, DateTime lastEdited)
        {
            return System.IO.File.ReadAllText(String.Format(@"{0}\App_plugins\uFPC\cache\{1}_{2}.cshtml", AppDomain.CurrentDomain.BaseDirectory, template.Alias, lastEdited.Ticks.ToString()));
        }

        public static string GetRelativePathFromCache(ITemplate template, DateTime lastEdited)
        {
            return String.Format(@"\App_plugins\uFPC\cache\{0}_{1}.cshtml", template.Alias, lastEdited.Ticks.ToString());
        }

        public static bool PathExists(ITemplate template, DateTime lastEdited)
        {
            return System.IO.File.Exists(String.Format(@"{0}\App_plugins\uFPC\cache\{1}_{2}.cshtml", AppDomain.CurrentDomain.BaseDirectory, template.Alias, lastEdited.Ticks.ToString()));
        }

        public static string FindViewStartTemplate()
        {
            Regex reg = new Regex(String.Format(@"{0}_.*.cshtml", "_ViewStart"), RegexOptions.IgnoreCase);
            var files = System.IO.Directory.GetFiles((String.Format(@"{0}Views\", AppDomain.CurrentDomain.BaseDirectory, "*.cshtml")));

            if (files.Count() > 0)
            {
                return System.IO.File.ReadAllText(files.First());
            } else
            {
                return String.Empty;
            }
        }

        public static string FindView(ITemplate template)
        {
            Regex reg = new Regex(String.Format(@"{0}_.*.cshtml", template.Alias));
            var files = System.IO.Directory.GetFiles((String.Format(@"{0}App_plugins\uFPC\cache\", AppDomain.CurrentDomain.BaseDirectory, "*.cshtml")));

            if (files != null)
            {
                return files.FirstOrDefault(path => reg.IsMatch(path));
            } else
            {
                return String.Empty;
            }
        }

        public static void RemoveFile(ITemplate template)
        {
            var view = FindView(template);
            
            if (!String.IsNullOrEmpty(view))
            {
                System.IO.File.Delete(view.Replace(@"\", @"/"));
            }
        }
    }
}