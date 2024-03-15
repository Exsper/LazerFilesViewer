using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Security.AccessControl;

namespace LazerFilesViewer.Localisation
{
    public class Language
    {
        public static void SetLocalClutrue(string? lang)
        {
            string[] LANGNAMES = { "en-US", "zh-CN" };
            if (string.IsNullOrEmpty(lang))
            {
                lang = CultureInfo.InstalledUICulture.Name;
            }
            if (!LANGNAMES.Contains(lang))
            {
                lang = LANGNAMES[0];
            }
            CultureInfo currentClutrue = new CultureInfo(lang);
            Thread.CurrentThread.CurrentCulture = currentClutrue;
        }

        public static string GetString(string key)
        {
            string s;
            try
            {
                ResourceManager resManager = new ResourceManager("LazerFilesViewer.Localisation.Resource", Assembly.GetExecutingAssembly());
                s = resManager.GetString(key, Thread.CurrentThread.CurrentCulture) ?? key;
            }
            catch
            {
                s = key;
            }
            return s;
        }
    }
}
