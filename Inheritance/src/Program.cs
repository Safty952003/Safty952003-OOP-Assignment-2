using LibrarySystem;

StudentMember student = new StudentMember(
    1,
    "Ahmed Ali",
    "01000000000");

PremiumMember premium = new PremiumMember(
    2,
    "Omar Hassan",
    "01111111111",
    10);

Book book = new Book(101, "Clean Code", 10);
DVD dvd = new DVD(102, "The Matrix", 10);
Magazine magazine = new Magazine(103, "Tech Monthly", 10);
Book anotherBook = new Book(104, "Design Patterns", 10);

// These lines must NOT compile

// Person person = new Person(1, "Ahmed", "01000000000");

// Member member = new Member(
//     2,
//     "Omar Hassan",
//     "01111111111",
//     3,
//     0);

// Staff staff = new Staff(
//     3,
//     "Ali",
//     "01222222222",
//     new DateTime(2026, 1, 1),
//     5000,
//     0);

// LibraryItem item = new LibraryItem(
//     101,
//     "Book",
//     10,
//     21,
//     1);

// student.FullName = "New Name";

// student.Loans.Add(null);

// book.IsOnLoan = true;


// Student borrows 3 items
student.Borrow(1, book, new DateTime(2026, 9, 27));
student.Borrow(2, dvd, new DateTime(2026, 9, 27));
student.Borrow(3, magazine, new DateTime(2026, 9, 27));

Console.WriteLine("Student borrowed 3 items successfully.");

// Student tries to borrow a 4th item
try
{
    student.Borrow(4, anotherBook, new DateTime(2026, 9, 27));
}
catch (Exception ex)
{
    Console.WriteLine($"4th loan rejected: {ex.Message}");
}

// Withdrawn item
Book withdrawnBook = new Book(105, "Old Book", 10);
withdrawnBook.Withdraw();

try
{
    premium.Borrow(5, withdrawnBook, new DateTime(2026, 9, 27));
}
catch (Exception ex)
{
    Console.WriteLine($"Withdrawn item rejected: {ex.Message}");
}

// Item already on loan
Book busyBook = new Book(106, "Popular Book", 10);

premium.Borrow(6, busyBook, new DateTime(2026, 9, 27));

try
{
    student.Borrow(7, busyBook, new DateTime(2026, 9, 27));
}
catch (Exception ex)
{
    Console.WriteLine($"Already-on-loan item rejected: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("=== Staff ===");

Librarian librarian = new Librarian(
    3,
    "Mahmoud Ali",
    "01222222222",
    new DateTime(2025, 1, 1),
    8000);

Shelver shelver = new Shelver(
    4,
    "Hassan Ahmed",
    "01233333333",
    new DateTime(2024, 6, 1),
    6000,
    "Fiction");

HeadLibrarian headLibrarian = new HeadLibrarian(
    5,
    "Sara Mohamed",
    "01244444444",
    new DateTime(2023, 3, 1),
    10000);

List<Staff> staffMembers = new List<Staff>
{
    librarian,
    shelver,
    headLibrarian
};

foreach (Staff staff in staffMembers)
{
    Console.WriteLine($"{staff.FullName}: {staff.GetMonthlyPay()}");
}


Console.WriteLine();
Console.WriteLine("=== Library Items ===");

List<LibraryItem> items = new List<LibraryItem>
{
    new Book(107, "C# Basics", 10),
    new DVD(108, "Inception", 10),
    new Magazine(109, "Science Monthly", 10)
};

foreach (LibraryItem item in items)
{
    Console.WriteLine(
        $"{item.Title}: {item.LoanPeriodDays} days, daily late fee = {item.GetDailyLateFee()}");
}

Console.WriteLine();
Console.WriteLine("=== Premium Member ===");

DVD lateDvd = new DVD(110, "Interstellar", 10);

Loan premiumLoan = premium.Borrow(
    8,
    lateDvd,
    new DateTime(2026, 9, 27));

// Return the DVD 5 days after the due date
DateTime returnDate = new DateTime(2026, 10, 9);

premiumLoan.Return(returnDate);

Console.WriteLine($"Due date: {premiumLoan.DueDate:d}");
Console.WriteLine($"Return date: {premiumLoan.ReturnDate:d}");
Console.WriteLine($"Late fee: {premiumLoan.LateFee}");
Console.WriteLine($"Reading points: {premium.ReadingPoints}");

Console.WriteLine();
Console.WriteLine("=== Invalid Loan Status Changes ===");

// Try to return the same loan twice
try
{
    premiumLoan.Return(new DateTime(2026, 10, 9));
}
catch (Exception ex)
{
    Console.WriteLine($"Second return rejected: {ex.Message}");
}

// Try to mark a returned loan as lost
try
{
    premiumLoan.MarkAsLost();
}
catch (Exception ex)
{
    Console.WriteLine($"Marking returned loan as lost rejected: {ex.Message}");
}