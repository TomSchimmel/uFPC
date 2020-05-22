using System;
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
    }
}