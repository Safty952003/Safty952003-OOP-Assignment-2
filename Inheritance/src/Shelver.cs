using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem;

public class Shelver : Staff
{
    public string Section { get; private set; }

    public Shelver(
        int personId,
        string fullName,
        string phone,
        DateTime hireDate,
        decimal monthlySalary,
        string section)
        : base(
            personId,
            fullName,
            phone,
            hireDate,
            monthlySalary,
            0)
    {
        Section = section;
    }

    public void Reassign(string newSection)
    {
        Section = newSection;
    }
}