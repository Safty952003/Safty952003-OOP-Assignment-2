using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem;

public class LibraryItem
{
    public int CatalogNumber { get; }
    public string Title { get; }

    public decimal BaseLateFee { get; private set; }

    public bool IsWithdrawn { get; private set; }

    public bool IsOnLoan { get; internal set; }

    public int LoanPeriodDays { get; }

    private decimal LateFeeMultiplier { get; }

    protected LibraryItem(
        int catalogNumber,
        string title,
        decimal baseLateFee,
        int loanPeriodDays,
        decimal lateFeeMultiplier)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.");

        if (baseLateFee <= 0)
            throw new ArgumentException("Late fee must be greater than zero.");

        CatalogNumber = catalogNumber;
        Title = title;
        BaseLateFee = baseLateFee;
        LoanPeriodDays = loanPeriodDays;
        LateFeeMultiplier = lateFeeMultiplier;
    }

    public void ChangeLateFee(decimal newBaseFee)
    {
        if (newBaseFee <= 0)
            throw new ArgumentException("Late fee must be greater than zero.");

        BaseLateFee = newBaseFee;
    }

    public void Withdraw()
    {
        IsWithdrawn = true;
    }

    public void Restore()
    {
        IsWithdrawn = false;
    }

    public decimal GetDailyLateFee()
    {
        return BaseLateFee * LateFeeMultiplier;
    }
}
