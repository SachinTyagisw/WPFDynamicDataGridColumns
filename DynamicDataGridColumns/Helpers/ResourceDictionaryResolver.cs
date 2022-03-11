using System;
using System.Windows;

namespace DynamicDataGridColumns.Helpers
{
    public static class ResourceDictionaryResolver
    {
        public static ResourceDictionary GetResourceDictionary(string uri)
        {
            ResourceDictionary resourceDictionary = null;
            foreach (ResourceDictionary resourceDictionaryScan in Application.Current.Resources.MergedDictionaries)
            {
                if (resourceDictionaryScan.Source.ToString().Contains(uri))
                {
                    resourceDictionary = resourceDictionaryScan;
                    break;
                }
            }

            return resourceDictionary;
        }
    }
}
