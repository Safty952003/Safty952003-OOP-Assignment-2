using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem;

public class Person
{
    public int PersonId { get; }
    public string FullName { get; }
    public string Phone { get; }

    protected Person(int personId, string fullName, string phone)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name cannot be empty.");

        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Phone cannot be empty.");

        PersonId = personId;
        FullName = fullName;
        Phone = phone;
    }
}
