using System.Collections.Generic;

namespace StreamControl.Core.Services
{
    public interface IPlaceholderService
    {
        Dictionary<string, string> Placeholders { get; set; }

        IEnumerable<string> ReplacePlaceholders(IEnumerable<string> texts, params object[] instances);
        string ReplacePlaceholders(string text, params object[] instances);
        void AddAll(IEnumerable<KeyValuePair<string, string>> placeholders);
    }
}