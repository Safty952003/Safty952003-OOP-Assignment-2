using System.Diagnostics;
using PatternsLab.Problems.Builder;
using PatternsLab.Problems.Prototype;
using PatternsLab.Problems.Singleton;

Console.WriteLine("=== SINGLETON: AFTER ===\n");

var db = new DatabaseService();
var ui = new UiService();

Console.WriteLine();

db.Config.Theme = "Dark";
Console.WriteLine("Admin changed theme to Dark.");

ui.Render();

Console.WriteLine($"\nSame config object? {ReferenceEquals(db.Config, ui.Config)}");
Console.WriteLine($"Times config was loaded from disk: {AppConfig.LoadCount}");

Console.WriteLine("\n=== PROTOTYPE: AFTER ===\n");

var sw = Stopwatch.StartNew();
var army = new List<Enemy>();

Enemy orcPrototype = new Orc();

for (int i = 1; i <= 5; i++)
{
    var orc = orcPrototype.Clone();
    orc.Name = $"Orc-{i}";
    army.Add(orc);
}
Console.WriteLine($"Created 5 orcs in {sw.ElapsedMilliseconds} ms\n");

Enemy original = new Orc();
original.Name = "Boss Orc";
Enemy copy = original.Clone();
Console.WriteLine($"\nOriginal model id: {original.ModelId}");
Console.WriteLine($"Copy model id:     {copy.ModelId}");

copy.Weapon.Damage = 999;
Console.WriteLine($"\nWe changed the COPY's weapon damage to 999.");
Console.WriteLine($"Original's weapon damage is now: {original.Weapon.Damage}");

copy.Abilities.Add("Fire Breath");
Console.WriteLine($"Original abilities: {string.Join(", ", original.Abilities)}");
var registry = new EnemyRegistry();

registry.Register("orc", orcPrototype);

Enemy registryOrc = registry.Create("orc");

Console.WriteLine($"\nRegistry created: {registryOrc.Name}");
Console.WriteLine($"Same model id: {registryOrc.ModelId == orcPrototype.ModelId}");

Console.WriteLine("\n=== BUILDER: AFTER ===\n");
Console.WriteLine(RegistrationCallSites.CreateLiveStudentUgly());
Console.WriteLine(RegistrationCallSites.CreateVideosOnlyUgly());
try
{
    new CourseRegistrationBuilder()
        .ForStudent("test@mail.com")
        .ForCourse("SEF-101")
        .WithAccessMode("LiveGroup")
        .Build();
}
catch (Exception ex)
{
    Console.WriteLine($"Validation: {ex.Message}");
}
try
{
    new CourseRegistrationBuilder()
        .ForStudent("test@mail.com")
        .ForCourse("SEF-101")
        .WithAccessMode("VideosOnly")
        .WithGroupCode("G1")
        .Build();
}
catch (Exception ex)
{
    Console.WriteLine($"Validation: {ex.Message}");
}
