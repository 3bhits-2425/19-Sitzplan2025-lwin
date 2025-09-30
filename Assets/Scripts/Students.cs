using UnityEngine;
using UnityEngine.UI;

public class Students : MonoBehaviour
{
    public string Name;
    public string ClassID;
    public string Grade;
    public bool IsPresent;

    private Button button;

    void Awake()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PrintStudentInfo);
        }
    }

    void PrintStudentInfo()
    {
        Debug.Log($"Name: {Name}, Class: {ClassID}, Grade: {Grade}, Present: {IsPresent}");
    }
}
