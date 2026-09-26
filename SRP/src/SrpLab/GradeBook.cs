namespace SrpLab;

/// <summary>
/// Grade book: aggregation, letter-band policy, and transcript export.
/// </summary>
public sealed class GradeBook
{
    private readonly Dictionary<string, List<decimal>> _scores = new(StringComparer.OrdinalIgnoreCase);
    private readonly GradePolicy _gradePolicy = new();

    public void Record(string studentId, decimal score)
    {
        if (score is < 0 or > 100) throw new ArgumentOutOfRangeException(nameof(score));
        if (!_scores.TryGetValue(studentId, out var list))
        {
            list = new List<decimal>();
            _scores[studentId] = list;
        }
        list.Add(score);
    }

    public decimal Average(string studentId)
    {
        if (!_scores.TryGetValue(studentId, out var list) || list.Count == 0) return 0m;
        return Math.Round(list.Average(), 2);
    }

    public string Letter(string studentId)
    {
        return _gradePolicy.GetLetter(Average(studentId));
    }

    public bool MeetsHonorRoll(string studentId)
    {
        // Extra academic rule set embedded beside averaging.
        return Average(studentId) >= 85 && Letter(studentId) is "A" or "B";
    }

    public string TranscriptPlain(string studentId, string fullName)
    {
        // Registrar document format ≠ grading policy.
        return $"TRANSCRIPT\nStudent: {fullName} ({studentId})\nAverage: {Average(studentId)}\nLetter: {Letter(studentId)}\nHonor: {MeetsHonorRoll(studentId)}\n";
    }

    public string ExportCsv()
    {
        var rows = new List<string> { "studentId,average,letter,honor" };
        foreach (var id in _scores.Keys.OrderBy(x => x))
            rows.Add($"{id},{Average(id)},{Letter(id)},{(MeetsHonorRoll(id) ? 1 : 0)}");
        return string.Join('\n', rows);
    }
}

public sealed class GradePolicy
{
    public string GetLetter(decimal average)
    {
        if (average >= 90) return "A";
        if (average >= 80) return "B";
        if (average >= 70) return "C";
        if (average >= 60) return "D";
        return "F";
    }
}