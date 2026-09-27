## OOP Assignment 2

## Student Information

- Name: Mahmoud Mohamed Mahmoud Elsafty
- Group: G1

---

## Assignment Overview

This assignment covers:

- Single Responsibility Principle (SRP)
- Creational Design Patterns
- Design With Inheritance
- LeetCode 1456 - Maximum Number of Vowels in a Substring of Given Length

---

## Task 1 - Single Responsibility Principle

The provided classes were refactored by separating their responsibilities into cohesive types while preserving the original behavior.

The responsibility analysis is available in:

`SRP/Responsibilities.md`

---

## Task 2 - Creational Design Patterns

Three creational design patterns were applied:

### Singleton

Applied to ensure that only one `AppConfig` object is created and shared across the application.

### Prototype

Applied to create new enemies by cloning an existing prototype instead of creating each object from scratch.

Deep copying was used for reference types such as `Weapon` and `Abilities`.

### Builder

Applied to simplify the creation of `CourseRegistration` objects with multiple required and optional parameters.

### LinkedIn Posts

The three LinkedIn posts are available in:

`DesignPatterns/linked.md`

---

## Task 3 - Design With Inheritance

A library system was designed and implemented using inheritance, encapsulation, and polymorphism.

The system includes:

- `Person`
- `Member`
- `StudentMember`
- `PremiumMember`
- `Staff`
- `Librarian`
- `Shelver`
- `HeadLibrarian`
- `LibraryItem`
- `Book`
- `DVD`
- `Magazine`
- `Loan`
- `LoanStatus`

The UML class diagram is available in:

`Inheritance/ClassDiagram.png`

The implementation also includes validation and controlled state changes for members, staff, loans, and library items.

---

## Task 4 - LeetCode

### 1456 - Maximum Number of Vowels in a Substring of Given Length

The problem was solved using a **fixed-size sliding window** approach.

The window keeps a constant size of `k` and moves one character at a time.

The solution:

- counts the vowels in the first window
- adds the new character entering the window
- removes the character leaving the window
- keeps track of the maximum number of vowels

### Links

- Problem: https://leetcode.com/problems/maximum-number-of-vowels-in-a-substring-of-given-length/
- Submission: https://leetcode.com/problems/maximum-number-of-vowels-in-a-substring-of-given-length/submissions/2155411205/
- LeetCode Profile: https://leetcode.com/u/l2lF93753u/

---

## Notes

- Each task is kept in its own folder.
- The inheritance class diagram was committed before the C# implementation.
- The LinkedIn posts are public and their links are stored in `DesignPatterns/linked.md`.
- The LeetCode submission link is stored in `leetcode.md`.
- Build output folders such as `bin/` and `obj/` are not included.
