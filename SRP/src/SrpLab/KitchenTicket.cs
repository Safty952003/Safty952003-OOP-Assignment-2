namespace SrpLab;

/// <summary>
/// Kitchen ticket: allergen scan from ingredients, cook ETA heuristics, and printed ticket layout.
/// </summary>
public sealed class KitchenTicket
{
    private readonly List<(string Item, List<string> Ingredients, int PrepMinutes)> _items = new();
    private readonly AllergenDetector _allergenDetector = new();
    private readonly PrepTimeCalculator _prepTimeCalculator = new();
    private readonly ThermalTicketRenderer _thermalTicketRenderer = new();
    private readonly ExpoLaneSelector _expoLaneSelector = new();

    public void AddItem(string item, IEnumerable<string> ingredients, int prepMinutes)
    {
        _items.Add((item, ingredients.Select(i => i.Trim().ToLowerInvariant()).ToList(), prepMinutes));
    }

    public IReadOnlyList<string> DetectAllergens()
    {
        return _allergenDetector.Detect(
            _items.Select(i => i.Ingredients));
    }

    public int EstimatedReadyMinutes(int openStations)
    {
        return _prepTimeCalculator.Calculate(
            _items,
            openStations,
            DetectAllergens().Count);
    }

    public string RenderThermalTicket(int orderNumber)
    {
        var allergens = DetectAllergens();

        return _thermalTicketRenderer.Render(
            orderNumber,
            _items,
            EstimatedReadyMinutes(2),
            allergens);
    }

    public string ExpoLaneHint()
    {
        return _expoLaneSelector.Select(
            DetectAllergens().Count,
            EstimatedReadyMinutes(2));
    }
}

public sealed class AllergenDetector
{
    public IReadOnlyList<string> Detect(
        IEnumerable<List<string>> ingredientsLists)
    {
        var hits = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var ingredients in ingredientsLists)
        {
            foreach (var ing in ingredients)
            {
                if (ing.Contains("milk") || ing.Contains("cheese") || ing.Contains("butter"))
                    hits.Add("dairy");

                if (ing.Contains("wheat") || ing.Contains("flour") || ing.Contains("bread"))
                    hits.Add("gluten");

                if (ing.Contains("peanut") || ing.Contains("almond") || ing.Contains("cashew"))
                    hits.Add("nuts");

                if (ing.Contains("shrimp") || ing.Contains("prawn") || ing.Contains("crab"))
                    hits.Add("shellfish");
            }
        }

        return hits.OrderBy(x => x).ToList();
    }
}
public sealed class PrepTimeCalculator
{
    public int Calculate(
        IEnumerable<(string Item, List<string> Ingredients, int PrepMinutes)> items,
        int openStations,
        int allergenCount)
    {
        if (openStations <= 0)
            openStations = 1;

        var itemList = items.ToList();

        var sequential = itemList.Sum(i => i.PrepMinutes);
        var parallel = (int)Math.Ceiling(sequential / (double)openStations);

        if (allergenCount > 0)
            parallel += 3;

        var longest = itemList.Count == 0
            ? 0
            : itemList.Max(i => i.PrepMinutes);

        return Math.Max(parallel, longest);
    }
}
public sealed class ThermalTicketRenderer
{
    public string Render(
        int orderNumber,
        IEnumerable<(string Item, List<string> Ingredients, int PrepMinutes)> items,
        int estimatedMinutes,
        IReadOnlyList<string> allergens)
    {
        var width = 32;
        var line = new string('=', width);

        var body = string.Join(
            '\n',
            items.Select(i => $"* {i.Item.ToUpperInvariant()} ({i.PrepMinutes}m)"));

        var allergyLine = allergens.Count == 0
            ? "ALLERGENS: none"
            : "ALLERGENS: " + string.Join(",", allergens);

        return $"{line}\nORDER #{orderNumber}\nETA {estimatedMinutes} MIN\n{body}\n{allergyLine}\n{line}\n";
    }
}
public sealed class ExpoLaneSelector
{
    public string Select(int allergenCount, int estimatedMinutes)
    {
        return allergenCount > 0
            ? "LANE-ALLERGY"
            : estimatedMinutes > 20
                ? "LANE-SLOW"
                : "LANE-FAST";
    }
}