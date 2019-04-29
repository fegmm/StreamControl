using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamControl.Core.Services
{
    public class PlaceholderService : IPlaceholderService
    {
        public Dictionary<string, string> Placeholders { get; set; }

        public string ReplacePlaceholders(string text, params object[] instances)
        {
            return ReplacePlaceholders(new string[] { text }, instances).First();
        }

        public IEnumerable<string> ReplacePlaceholders(IEnumerable<string> texts, params object[] instances)
        {
            Dictionary<string, string> propertyPlaceholders = GetPropertyPlaceholders(instances);

            foreach (var item in texts)
            {
                StringBuilder sb = new StringBuilder(item);
                foreach (var placeholder in Placeholders
                                                .Concat(propertyPlaceholders)
                                                .Prepend(ReplaceDatePlaceholder(item)))
                    sb.Replace(placeholder.Key, placeholder.Value);
                yield return sb.ToString();
            }
        }

        private KeyValuePair<string, string> ReplaceDatePlaceholder(string item)
        {
            int start = item.IndexOf("$Date{") + 6;
            if (start < 0)
                return new KeyValuePair<string, string>("", "");

            int end = item.IndexOf("}", start);
            var format = item.Substring(start, end - start);
            return new KeyValuePair<string, string>("$Date{" + format + "}", DateTime.Now.ToString(format));
        }

        private static Dictionary<string, string> GetPropertyPlaceholders(object[] instances)
        {
            Dictionary<string, string> propertyPlaceholders = new Dictionary<string, string>();
            foreach (var item in instances.Reverse())
                foreach (var property in item.GetType().GetProperties(System.Reflection.BindingFlags.Public))
                    propertyPlaceholders[property.Name] = property.GetValue(item).ToString();
            return propertyPlaceholders;
        }
    }
}
