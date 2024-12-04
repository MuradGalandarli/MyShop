using Azure.Core;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLayer.Helpers
{
    public static class TokenHelper
    {
        public static string ProcessToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                string[] parts = token.Split('.');
                string payload = Encoding.UTF8.GetString(Convert.FromBase64String(parts[1]));
                string[] nameId = payload.Split('"');
                Console.WriteLine(nameId[11]);
                Console.WriteLine(JsonDocument.Parse(payload).RootElement);
                return nameId[11];
            }
            return null;
        }
    }
}
