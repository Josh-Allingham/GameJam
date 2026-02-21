using UnityEngine;
using System.Collections.Generic;
public class Table : MonoBehaviour
{

    public int tableNumber;
    public int numSeats;
    public bool[] seatsHaveFood;
    public Vector3[] dishPositions;
    private List<Customer> customers;

    
    void Start()
    {
        seatsHaveFood = new bool[numSeats];
        dishPositions = new Vector3[numSeats];    
        customers = new List<Customer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
