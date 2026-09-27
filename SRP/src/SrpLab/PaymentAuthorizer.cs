using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public sealed class PaymentAuthorizer
{
    public string Authorize(decimal total, string cardLast4, int itemCount)
    {
        var payload = $"{total:0.00}|{cardLast4}|{itemCount}";
        var hash = payload.GetHashCode();

        return $"AUTH-{Math.Abs(hash):X8}";
    }
}
