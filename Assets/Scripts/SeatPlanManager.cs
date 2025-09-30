using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SeatPlanManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject DeskPrefab;
    public GameObject ChairPrefab;
    public GameObject StudentsPrefab; // Button Prefab

    [Header("Grid Settings")]
    public Transform gridParent; // z.B. ein Panel in Canvas
    public int rows = 3;
    public int columns = 8;

    [Header("Student Data (manuell eintragbar)")]
    public List<string> names;
    public List<string> classIDs;
    public List<string> grades;

    private Desk[,] desks;
    private Chair[,] chairs;

    void Start()
    {
        // Alte Desks löschen
        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        // Listen prüfen
        if (names.Count == 0 || classIDs.Count == 0 || grades.Count == 0)
        {
            Debug.LogError("Bitte fülle die Listen (names, classIDs, grades)!");
            return;
        }

        GenerateSeatPlan();
        AssignStudents();
    }

    void GenerateSeatPlan()
    {
        desks = new Desk[rows, columns];
        chairs = new Chair[rows, columns];

        int seatNumber = 1;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                // Desk erstellen
                GameObject deskObj = Instantiate(DeskPrefab, gridParent);
                Desk desk = deskObj.GetComponent<Desk>();
                if(desk == null)
                    Debug.LogWarning("Desk-Komponente fehlt auf Prefab!");
                else
                    desk.Number = seatNumber;

                desks[r, c] = desk;

                // Chair erstellen (unter Desk)
                GameObject chairObj = Instantiate(ChairPrefab, deskObj.transform);
                Chair chair = chairObj.GetComponent<Chair>();
                if(chair == null)
                    Debug.LogWarning("Chair-Komponente fehlt auf Prefab!");
                else
                    chair.Number = seatNumber;

                chairs[r, c] = chair;

                seatNumber++;
            }
        }
    }

    void AssignStudents()
    {
        int totalSeats = rows * columns;

        for (int i = 0; i < totalSeats; i++)
        {
            int r = i / columns;
            int c = i % columns;

            if (chairs[r, c] == null)
            {
                Debug.LogError($"Chair at {r},{c} is null! Student wird übersprungen.");
                continue;
            }

            // Student Button unter Chair erstellen
            GameObject studentObj = Instantiate(StudentsPrefab, chairs[r, c].transform);
            studentObj.transform.localPosition = Vector3.zero;
            studentObj.transform.localScale = Vector3.one;

            // Student-Daten setzen
            Students student = studentObj.GetComponent<Students>();
            if(student == null)
            {
                Debug.LogError("Students-Komponente fehlt auf Prefab!");
                continue;
            }

            student.Name = names[i % names.Count];
            student.ClassID = classIDs[i % classIDs.Count];
            student.Grade = grades[i % grades.Count];
            student.IsPresent = Random.value > 0.2f; // zufällig anwesend/abwesend

            // Farbe des Buttons (Image) nach Anwesenheit
            Image img = studentObj.GetComponent<Image>();
            if(img != null)
                img.color = student.IsPresent ? Color.green : Color.red;
            else
                Debug.LogWarning("Image-Komponente fehlt auf Students-Prefab!");
        }
    }
}
