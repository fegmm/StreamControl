using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StreamControl.Core
{
    public static class Extensions
    {
        public static T Load<T>(this T o, string fileName)
        {
            JsonConvert.PopulateObject(File.ReadAllText(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\" + fileName), o);
            return o;
        }
    }
}
