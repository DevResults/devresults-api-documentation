using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DevResults.Api.CSharpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var secretBytes = System.Text.Encoding.UTF8.GetBytes("~~yourApiSecret~~");
            var token = "~~yourApiToken~~";
            var ms = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
            var urlBase = string.Format("http://demo.devresults.com/api/awards?t={0}&ms={1}", token, ms);
            var uriBuilder = new UriBuilder(urlBase);

            byte[] hashBytes;

            var queryValues = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            var sortedKeys = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (string param in queryValues)
            {
                sortedKeys.Add(param, queryValues[param]);
            }

            var signature = sortedKeys.Aggregate("", (s, kvp) => s + string.Format("{0}|{1}|", kvp.Key, kvp.Value));
            var signatureBytes = System.Text.Encoding.UTF8.GetBytes(signature);
            using (var algorithm = HMAC.Create("HMACSHA256"))
            {
                algorithm.Key = secretBytes;
                hashBytes = algorithm.ComputeHash(signatureBytes);
            }

            var hashedSignature = BitConverter.ToString(hashBytes).ToLower().Replace("-", "");

            queryValues.Add("s", hashedSignature);
            uriBuilder.Query = queryValues.ToString();
            var apiUrl = uriBuilder.Uri.ToString();

            Console.WriteLine("Requesting: {0}", apiUrl);

            try
            {
                var client = new WebClient();
                var data = client.OpenRead(apiUrl);
                var reader = new StreamReader(data);
                var response = reader.ReadToEnd();

                Console.WriteLine("Success");
            }
            catch (Exception)
            {
                Console.WriteLine("Failure");
            }

            Console.ReadKey();
        }
    }
}
