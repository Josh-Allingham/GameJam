using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class Table : MonoBehaviour
{

    public int tableNumber;
    public int numSeats;
    public bool[] seatsHaveFood;
    public Vector3[] dishPositions;
    private List<Customer> customers;
    public TMP_Text tableNumberText;
    
    void Start()
    {
        seatsHaveFood = new bool[numSeats];
        dishPositions = new Vector3[numSeats];    
        customers = new List<Customer>();

        tableNumberText.text = tableNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
