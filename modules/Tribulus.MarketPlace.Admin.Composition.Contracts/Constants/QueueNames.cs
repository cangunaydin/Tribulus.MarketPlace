using System;
using Tribulus.MarketPlace.Admin.Utils;

namespace Tribulus.MarketPlace.Admin.Constants
{
    public class QueueNames
    {
        public const string SagaOrderPayment = "withdraw-customer-credit";
        private const string inMemoryUri = "loopback://localhost/";
        public static Uri GetMessageUri(string key)
        {
            return new Uri(inMemoryUri + key.PascalToKebabCaseMessage());
        }
        public static Uri GetActivityUri(string key)
        {
            var kebabCase = key.PascalToKebabCaseActivity();
            if (kebabCase.EndsWith('-'))
            {
                kebabCase = kebabCase.Remove(kebabCase.Length - 1);
            }
            return new Uri(inMemoryUri + kebabCase + '_' + "execute");
        }
    }
}