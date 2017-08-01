using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Names
{
    private static Names instance;

    private List<string> firstNames = new List<string>();
    private List<string> lastNames = new List<string>();

    private Names() {
        firstNames.Add("Arthur");
        firstNames.Add("Donald");
        firstNames.Add("Kenny");
        firstNames.Add("Ronald");
        firstNames.Add("James");

        lastNames.Add("Smith");
        lastNames.Add("Benson");
        lastNames.Add("Anderson");
        lastNames.Add("Jones");
        lastNames.Add("Baker");
        lastNames.Add("Wilson");
    }

    public static Names Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Names();
            }
            return instance;
        }
    }

    public string GetFirstName()
    {
        return firstNames[UnityEngine.Random.Range(0, firstNames.Count)];
    }

    public string GetLastName()
    {
        return lastNames[UnityEngine.Random.Range(0, lastNames.Count)];
    }
}
