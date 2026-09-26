using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public sealed class GiftWrapCalculator
{
    public decimal Calculate(bool enabled)
    {
        return enabled ? 4.99m : 0m;
    }
}
