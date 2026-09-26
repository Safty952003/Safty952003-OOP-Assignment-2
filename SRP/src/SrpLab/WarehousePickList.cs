namespace SrpLab;

/// <summary>
/// Warehouse pick list: naive path order, stock allocation, and picker instruction prose.
/// </summary>
public sealed class WarehousePickList
{
    private readonly List<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> _lines = new();
    private readonly InventoryAllocator _inventoryAllocator = new();
    private readonly WalkingOrderPlanner _walkingOrderPlanner = new();

    public void AddNeed(string sku, string aisle, int bin, int qtyNeeded, int qtyOnHand)
    {
        _lines.Add((sku, aisle, bin, qtyNeeded, qtyOnHand));
    }

    public IReadOnlyList<(string Sku, int Allocated)> Allocate()
    {
        return _inventoryAllocator.Allocate(_lines);
    }

    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> WalkingOrder()
    {
        return _walkingOrderPlanner.Plan(_lines);
    }

    public string PickerScript()
    {
        // UX wording for handheld devices — separate owners.
        var steps = WalkingOrder()
            .Select((s, i) => $"{i + 1}. Go aisle {s.Aisle} bin {s.Bin}: pick {s.Qty} × {s.Sku}");
        var shortfalls = Allocate().Where(a =>
        {
            var need = _lines.First(l => l.Sku == a.Sku).QtyNeeded;
            return a.Allocated < need;
        });
        var warn = shortfalls.Any()
            ? "SHORTAGES: " + string.Join(", ", shortfalls.Select(s => s.Sku))
            : "SHORTAGES: none";
        return string.Join('\n', steps) + "\n" + warn;
    }

    public string WmsXmlBatch(string batchId)
    {
        // Integration contract with WMS — third reason to change.
        var parts = Allocate().Select(a => $"<line sku=\"{a.Sku}\" qty=\"{a.Allocated}\" />");
        return $"<batch id=\"{batchId}\">{string.Join("", parts)}</batch>";
    }
}

public sealed class InventoryAllocator
{
    public IReadOnlyList<(string Sku, int Allocated)> Allocate(
        IEnumerable<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> lines)
    {
        var result = new List<(string, int)>();

        foreach (var line in lines)
        {
            var allocated = Math.Min(line.QtyNeeded, line.QtyOnHand);
            result.Add((line.Sku, allocated));
        }

        return result;
    }
}
public sealed class WalkingOrderPlanner
{
    public IReadOnlyList<(string Aisle, int Bin, string Sku, int Qty)> Plan(
        IEnumerable<(string Sku, string Aisle, int Bin, int QtyNeeded, int QtyOnHand)> lines)
    {
        return lines
            .OrderBy(l => l.Aisle)
            .ThenBy(l => l.Bin)
            .Select(l => (
                l.Aisle,
                l.Bin,
                l.Sku,
                Math.Min(l.QtyNeeded, l.QtyOnHand)))
            .Where(x => x.Item4 > 0)
            .ToList();
    }
}