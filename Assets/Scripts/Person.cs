using UnityEngine;

public abstract class Person : MonoBehaviour
{
    public int ID;
    public string Vorname;
    public string Nachname;

    public string GetFullName()
    {
        return Vorname + " " + Nachname;
    }
}
