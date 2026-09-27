using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem;

public class HeadLibrarian : Staff
{
    public HeadLibrarian(
        int personId,
        string fullName,
        string phone,
        DateTime hireDate,
        decimal monthlySalary)
        : base(
            personId,
            fullName,
            phone,
            hireDate,
            monthlySalary,
            400)
    {
    }

    public void ChangeLateFee(LibraryItem item, decimal newBaseFee)
    {
        item.ChangeLateFee(newBaseFee);
    }

    public void WithdrawItem(LibraryItem item)
    {
        item.Withdraw();
    }

    public void RestoreItem(LibraryItem item)
    {
        item.Restore();
    }
}