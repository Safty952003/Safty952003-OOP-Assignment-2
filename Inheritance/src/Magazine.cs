using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem;

public class Magazine : LibraryItem
{
    public Magazine(
        int catalogNumber,
        string title,
        decimal baseLateFee)
        : base(
            catalogNumber,
            title,
            baseLateFee,
            3,
            0.5m)
    {
    }
}
