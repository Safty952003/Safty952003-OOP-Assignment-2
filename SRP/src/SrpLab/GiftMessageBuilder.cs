using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public sealed class GiftMessageBuilder
{
    public string Build(string fromName, IEnumerable<string> items, decimal total)
    {
        var itemList = string.Join(", ", items);

        return $"Dear friend,\nA gift from {fromName} awaits ({itemList}).\n" +
               $"Total surprise value: {total:C}\n";
    }
}
