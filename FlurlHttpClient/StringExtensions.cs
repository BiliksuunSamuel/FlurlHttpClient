using Newtonsoft.Json;

namespace FlurlHttpClient;

public static class StringExtensions
{
   public static T FromJsonString<T>(this string json)
   {
       //if the typeof T is a string, return the json string
         if (typeof(T) == typeof(string))
         {
              return (T)(object)json;
         }
       return JsonConvert.DeserializeObject<T>(json);
   }
}