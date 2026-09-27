using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem;

public class Member : Person
{
    private readonly List<Loan> _loans = new();

    public IReadOnlyList<Loan> Loans => _loans;

    public int MaxLoans { get; }
    public decimal DiscountPercentage { get; }

    protected Member(
        int personId,
        string fullName,
        string phone,
        int maxLoans,
        decimal discountPercentage)
        : base(personId, fullName, phone)
    {
        MaxLoans = maxLoans;
        DiscountPercentage = discountPercentage;
    }

    public Loan Borrow(int loanId, LibraryItem item, DateTime borrowDate)
    {
        int activeLoans = 0;

        foreach (Loan loan in Loans)
        {
            if (loan.Status == LoanStatus.Borrowed)
            {
                activeLoans++;
            }
        }

        if (activeLoans >= MaxLoans)
            throw new InvalidOperationException("Member has reached the maximum loan limit.");

        if (item.IsWithdrawn)
            throw new InvalidOperationException("Withdrawn items cannot be borrowed.");

        if (item.IsOnLoan)
            throw new InvalidOperationException("Item is already on loan.");

        Loan newLoan = new Loan(loanId, borrowDate, this, item);

        _loans.Add(newLoan);
        item.IsOnLoan = true;

        return newLoan;
    }
}
