namespace SrpLab;

/// <summary>
/// Grade book: aggregation, letter-band policy, and transcript export.
/// </summary>
public sealed class GradeBook
{
    private readonly Dictionary<string, List<decimal>> _scores = new(StringComparer.OrdinalIgnoreCase);
    private readonly GradePolicy _gradePolicy = new();
    private readonly HonorRollPolicy _honorRollPolicy = new();
    private readonly TranscriptBuilder _transcriptBuilder = new();
    private readonly GradeBookCsvExporter _csvExporter = new();

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
        var average = Average(studentId);
        var letter = Letter(studentId);

        return _honorRollPolicy.Meets(average, letter);
    }

    public string TranscriptPlain(string studentId, string fullName)
    {
        return _transcriptBuilder.Build(
            studentId,
            fullName,
            Average(studentId),
            Letter(studentId),
            MeetsHonorRoll(studentId));
    }

    public string ExportCsv()
    {
        return _csvExporter.Export(
            _scores.Keys,
            Average,
            Letter,
            MeetsHonorRoll);
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
public sealed class HonorRollPolicy
{
    public bool Meets(decimal average, string letter)
    {
        return average >= 85 && letter is "A" or "B";
    }
}
public sealed class TranscriptBuilder
{
    public string Build(
        string studentId,
        string fullName,
        decimal average,
        string letter,
        bool honor)
    {
        return $"TRANSCRIPT\nStudent: {fullName} ({studentId})\n" +
               $"Average: {average}\n" +
               $"Letter: {letter}\n" +
               $"Honor: {honor}\n";
    }
}
public sealed class GradeBookCsvExporter
{
    public string Export(
        IEnumerable<string> studentIds,
        Func<string, decimal> average,
        Func<string, string> letter,
        Func<string, bool> honor)
    {
        var rows = new List<string> { "studentId,average,letter,honor" };

        foreach (var id in studentIds.OrderBy(x => x))
        {
            rows.Add(
                $"{id},{average(id)},{letter(id)},{(honor(id) ? 1 : 0)}");
        }

        return string.Join('\n', rows);
    }
}