namespace SrpLab;

/// <summary>
/// Online checkout basket: totals, coupon linguistics, gift-wrap copy, and a faux payment auth code.
/// </summary>
public sealed class CheckoutBasket
{
    private readonly List<(string Sku, decimal Price, int Qty)> _lines = new();
    private string? _couponRaw;
    private bool _giftWrap;
    private readonly CouponCalculator _couponCalculator = new();
    private readonly GiftMessageBuilder _giftMessageBuilder = new();
    private readonly PaymentAuthorizer _paymentAuthorizer = new();
    private readonly GiftWrapCalculator _giftWrapCalculator = new();

    public void AddLine(string sku, decimal price, int qty)
    {
        if (qty <= 0) throw new ArgumentOutOfRangeException(nameof(qty));
        _lines.Add((sku, price, qty));
    }

    public void ApplyCouponText(string? couponText) => _couponRaw = couponText;
    public void EnableGiftWrap() => _giftWrap = true;

    public decimal SubTotal() => _lines.Sum(l => l.Price * l.Qty);

    public decimal DiscountAmount()
    {
        return _couponCalculator.Calculate(_couponRaw, SubTotal());
    }

    public decimal GrandTotal()
    {
        var total = SubTotal() - DiscountAmount();
        total += _giftWrapCalculator.Calculate(_giftWrap);

        return Math.Max(0m, total);
    }

    public string GiftMessageCard(string fromName)
    {
        var items = _lines.Select(l => l.Sku);

        return _giftMessageBuilder.Build(
            fromName,
            items,
            GrandTotal());
    }

    public string AuthorizePaymentStub(string cardLast4)
    {
        return _paymentAuthorizer.Authorize(
            GrandTotal(),
            cardLast4,
            _lines.Count);
    }
}
