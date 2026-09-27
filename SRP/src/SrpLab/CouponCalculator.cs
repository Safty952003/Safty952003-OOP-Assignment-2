using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public sealed class CouponCalculator
{
    public decimal Calculate(string? couponText, decimal subtotal)
    {
        if (string.IsNullOrWhiteSpace(couponText))
            return 0m;

        var t = couponText.Trim().ToUpperInvariant();

        if (t.StartsWith("SAVE") &&
            int.TryParse(t[4..], out var pct) &&
            pct is > 0 and <= 50)
        {
            return Math.Round(subtotal * pct / 100m, 2);
        }

        if (t.Contains("FREESHIP"))
            return 0m;

        if (t == "WELCOME10")
            return Math.Min(10m, subtotal);

        return 0m;
    }
}
