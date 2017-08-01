using UnityEngine;
using System.Collections;

public class Employee
{
    public Profession Profession { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public float Experience { get; set; }

    public Employee()
    {
        FirstName = Names.Instance.GetFirstName();
        LastName = Names.Instance.GetLastName();
    }

    public string GetDisplayName()
    {
        return LastName + ", " + FirstName;
    }
}
