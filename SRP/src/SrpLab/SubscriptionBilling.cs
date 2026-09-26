namespace SrpLab;

/// <summary>
/// Subscription billing: proration math, invoice number minting, and dunning email bodies.
/// </summary>
public sealed class SubscriptionBilling
{
    private readonly InvoiceNumberGenerator _invoiceNumberGenerator = new();
    public string CustomerId { get; }
    public decimal MonthlyPrice { get; }
    public DateOnly PeriodStart { get; }
    public DateOnly PeriodEnd { get; }
    public int FailedPayments { get; private set; }
    private readonly ProrationCalculator _prorationCalculator = new();
    private readonly DunningEmailBuilder _dunningEmailBuilder = new();
    private readonly LedgerJournalBuilder _ledgerJournalBuilder = new();

    public SubscriptionBilling(string customerId, decimal monthlyPrice, DateOnly periodStart, DateOnly periodEnd)
    {
        CustomerId = customerId;
        MonthlyPrice = monthlyPrice;
        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
    }

    public decimal Prorate(DateOnly activeFrom)
    {
        return _prorationCalculator.Calculate(
            MonthlyPrice,
            PeriodStart,
            PeriodEnd,
            activeFrom);
    }

    public string NextInvoiceNumber()
    {
        return _invoiceNumberGenerator.Generate(PeriodStart);
    }

    public void RegisterFailedPayment() => FailedPayments++;

    public string DunningEmail(string customerName, DateOnly asOf)
    {
        var amount = Prorate(PeriodStart);
        var invoice = NextInvoiceNumber();

        return _dunningEmailBuilder.Build(
            customerName,
            asOf,
            amount,
            invoice,
            FailedPayments);
    }

    public string LedgerJournalLine(DateOnly activeFrom)
    {
        return _ledgerJournalBuilder.Build(
            CustomerId,
            NextInvoiceNumber(),
            Prorate(activeFrom));
    }
}

public sealed class ProrationCalculator
{
    public decimal Calculate(
        decimal monthlyPrice,
        DateOnly periodStart,
        DateOnly periodEnd,
        DateOnly activeFrom)
    {
        if (activeFrom <= periodStart)
            return monthlyPrice;

        if (activeFrom >= periodEnd)
            return 0m;

        var totalDays = periodEnd.DayNumber - periodStart.DayNumber;

        if (totalDays <= 0)
            return monthlyPrice;

        var used = periodEnd.DayNumber - activeFrom.DayNumber;

        return Math.Round(monthlyPrice * used / totalDays, 2);
    }
}
public sealed class InvoiceNumberGenerator
{
    private static int _invoiceSeq = 1000;

    public string Generate(DateOnly periodStart)
    {
        var n = ++_invoiceSeq;
        return $"INV-{periodStart:yyyyMM}-{n:D5}";
    }
}
public sealed class DunningEmailBuilder
{
    public string Build(
        string customerName,
        DateOnly asOf,
        decimal amount,
        string invoice,
        int failedPayments)
    {
        var severity = failedPayments switch
        {
            <= 1 => "friendly reminder",
            2 => "second notice",
            _ => "final notice before suspension"
        };

        return $"Subject: {severity} {invoice}\n" +
               $"Hi {customerName},\n" +
               $"Balance {amount:C} as of {asOf:o} ({failedPayments} failures).\n";
    }
}
public sealed class LedgerJournalBuilder
{
    public string Build(
        string customerId,
        string invoiceNumber,
        decimal amount)
    {
        return $"{customerId},{invoiceNumber},{amount:0.00},AR-SUB";
    }
}