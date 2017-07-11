using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BistroFiftyTwo.Server.Repositories
{
    public static class ObjectToDictionaryHelper
    {
        public static IDictionary<string, object> ToDictionary(this object source)
        {
            return source.ToDictionary<object>();
        }

        public static IDictionary<string, T> ToDictionary<T>(this object source)
        {
            if (source == null)
                ThrowExceptionWhenSourceArgumentIsNull();

            var dictionary = new Dictionary<string, T>();
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
                AddPropertyToDictionary(property, source, dictionary);
            return dictionary;
        }

        //public static DynamicParameters ToDynamicParameters(this object source)
        //{
        //    if (source == null)
        //        ThrowExceptionWhenSourceArgumentIsNull();

        //    var dynParm = new DynamicParameters();
        //    foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
        //    {
        //        if(property.)
        //    }
        //    return dictionary;
        //}
        private static void AddPropertyToDictionary<T>(System.ComponentModel.PropertyDescriptor property, object source,
            Dictionary<string, T> dictionary)
        {
            var value = property.GetValue(source);
            if (IsOfType<T>(value))
                dictionary.Add(property.Name, (T) value);
        }

        private static bool IsOfType<T>(object value)
        {
            return value is T;
        }

        private static void ThrowExceptionWhenSourceArgumentIsNull()
        {
            throw new ArgumentNullException("source",
                "Unable to convert object to a dictionary. The source object is null.");
        }
    }
}