using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem;

public class Librarian : Staff
{
    public Librarian(
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
            0)
    {
    }

    public void ReturnLoan(Loan loan, DateTime returnDate)
    {
        loan.Return(returnDate);
    }

    public void MarkAsLost(Loan loan)
    {
        loan.MarkAsLost();
    }
}