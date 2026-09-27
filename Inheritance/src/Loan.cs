using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem;

public enum LoanStatus
{
    Borrowed,
    Returned,
    Lost
}

public class Loan
{
    public int LoanId { get; }
    public DateTime BorrowDate { get; }
    public Member Member { get; }
    public LibraryItem Item { get; }

    public LoanStatus Status { get; private set; }

    public DateTime DueDate
    {
        get
        {
            return BorrowDate.AddDays(Item.LoanPeriodDays);
        }
    }

    public DateTime? ReturnDate { get; private set; }

    public decimal LateFee
    {
        get
        {
            if (!ReturnDate.HasValue || ReturnDate <= DueDate)
                return 0;

            int lateDays = (ReturnDate.Value - DueDate).Days;
            decimal fee = lateDays * Item.GetDailyLateFee();

            return fee - (fee * Member.DiscountPercentage / 100);
        }
    }

    public Loan(
        int loanId,
        DateTime borrowDate,
        Member member,
        LibraryItem item)
    {
        LoanId = loanId;
        BorrowDate = borrowDate;
        Member = member;
        Item = item;
        Status = LoanStatus.Borrowed;
    }

    public void Return(DateTime returnDate)
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException("Only borrowed loans can be returned.");

        if (returnDate < BorrowDate)
            throw new ArgumentException("Return date cannot be earlier than borrow date.");

        ReturnDate = returnDate;
        Status = LoanStatus.Returned;
        Item.IsOnLoan = false;
    }

    public void MarkAsLost()
    {
        if (Status != LoanStatus.Borrowed)
            throw new InvalidOperationException("Only borrowed loans can be marked as lost.");

        Status = LoanStatus.Lost;
        Item.IsOnLoan = false;
    }
}