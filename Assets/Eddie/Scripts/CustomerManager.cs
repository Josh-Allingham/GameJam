using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CustomerManager : MonoBehaviour
{
    public static CustomerManager main;
    public List<GameObject> customerList = new List<GameObject>();
    private List<GameObject> customersToDelete = new List<GameObject>();
    public int[,] tableSeatIndex = { { 2, 0 },{3, 0 },{3, 0 },{2, 0 },{4, 0 },{2, 0 } };
    public Vector4[] tableSeatLocations = {new Vector4(18f,1f,0f,-115f),
        new Vector4(8f,1f,-3f,-160f),
        new Vector4(17f,1f,-11f,-150f),
        new Vector4(-15.75f, 1f, -18.6f, 0f),
        new Vector4(-12f, 1f, -9f, 0f),
        new Vector4(-16f, 1f, 2f, 0f)};
    [SerializeField] private GameObject[] NPCPrefabs;
    float customerArriveCountdown = 5f;
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCustomers();

        NPCMovement();

        DestroyMarkedNPCs();
        
    }

    void SpawnCustomers()
    {
        if (customerArriveCountdown > 0f)
        {
            customerArriveCountdown -= Time.deltaTime;
        }

        //if there is a free table
        for (int i = 0; i < tableSeatIndex.GetLength(0); i++)
        {
            if (tableSeatIndex[i, 0] != tableSeatIndex[i, 1])
            {
                //free table
                if (customerArriveCountdown <= 0f)
                {
                    SpawnNPC(i);
                    tableSeatIndex[i, 1] = tableSeatIndex[i, 0];
                    customerArriveCountdown = 10f;
                }

            }
        }
    }
    void NPCMovement()
    {
        if (customerList != null)
        {
            foreach (GameObject obj in customerList)
            {
                if (obj != null && obj.TryGetComponent(out Customer customer))
                {
                    if (!customer.isSeated)
                    {
                        //Sit them in the chair
                        customer.transform.position = new Vector3(tableSeatLocations[customer.table].x, tableSeatLocations[customer.table].y, tableSeatLocations[customer.table].z);
                        customer.transform.localEulerAngles = new Vector3(0, tableSeatLocations[customer.table].w, 0);
                    }
                    else
                    {
                        WalkForwards(customer);
                    }
                }
            }
        }
    }
    void DestroyMarkedNPCs()
    {
        foreach (GameObject cust in customersToDelete)
        {
            Destroy(cust);
        }
    }
    void SpawnNPC(int tableIndex)
    {
        //int numNPCs = tableSeatIndex[tableIndex, 1];
        int numNPCs = 1;
        for (int i = 0; i < numNPCs; i++)
        {
            GameObject newCustomer = Instantiate(NPCPrefabs[Random.Range(0, NPCPrefabs.Length)], new Vector3(-0.25f, 2f, -20f), Quaternion.identity);
            newCustomer.GetComponent<Customer>().table = tableIndex;
            newCustomer.GetComponent<Customer>().seatPosition = tableSeatLocations[tableIndex];
            customerList.Add(newCustomer);
        }

        
    }

    private void WalkForwards(Customer customer)
    {
        customer.transform.position += Vector3.forward * Time.deltaTime;
        if (Vector3.Distance(customer.transform.position, new Vector3(-0.25f, 2f, -20f)) > 20)
        {
            customer.isSeated = true;
        }

    }

    public void GiveFood(int tableIndex)
    {
        Debug.Log("GIVE FOOD");
        foreach (GameObject obj in customerList)
        {
            if (obj != null)
            {
                Customer cust = obj.GetComponent<Customer>();
                if (cust.table == tableIndex)
                {
                    cust.hasFood = true;
                    HUDScript.main.tableServed();
                }
                
            }
            
        }
    }
    public void ClearTable(int tableIndex) 
    {
        customersToDelete.Clear();
        tableSeatIndex[tableIndex, 1] = 0;
        foreach (GameObject obj in customerList)
        {
            if (obj != null)
            {
                Customer cust = obj.GetComponent<Customer>();
                if (cust.table == tableIndex)
                {
                    customersToDelete.Add(cust.gameObject);
                }

            }
        }
    }


   
}
