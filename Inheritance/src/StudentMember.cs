using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem;

public class StudentMember : Member
{
    public StudentMember(
        int personId,
        string fullName,
        string phone)
        : base(personId, fullName, phone, 3, 0)
    {
    }
}
