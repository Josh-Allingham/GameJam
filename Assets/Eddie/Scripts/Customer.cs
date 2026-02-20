using UnityEngine;
using System.Collections;

public class Customer : MonoBehaviour
{
    public int table;
    public Vector4 seatPosition;
    public bool isSeated;
    public bool hasOrdered;
    public bool hasEaten;
    public bool hasFood;
    public Vector3 spawnPoint = new Vector3(-0.25f, 2f, -20f);
    private float walkTimer = 2f;

    public Customer(int table, Vector3 seatPosition)
    {
        this.table = table;
        this.seatPosition = seatPosition;
        transform.position = spawnPoint;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = spawnPoint;
        hasEaten = false;
        isSeated = false;
        hasFood = false;
        hasOrdered = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isSeated)
        {
            walkTimer -= Time.deltaTime;
            transform.position += Vector3.forward;
            if (walkTimer < 0)
            {
                walkTimer = 2f;
                isSeated = true;
                transform.position = new Vector3(seatPosition.x, seatPosition.y, seatPosition.z);
                transform.localEulerAngles = new Vector3(0,seatPosition.w,0);
            }
        }
        else if (!hasOrdered)
        {
            GetComponentInChildren<Animator>().SetBool("IsSatDown", true);

            //request dish
            DishObject newInstance = Instantiate(PlayerTray.main.dishPrefab, DishObject.dishSpawnPos, Quaternion.identity);
            newInstance.ImportSettings("New Dish", table + 1, 1);
            hasOrdered = true;
            //get dish

            
        }
        //eat dish
        else if (hasFood)
        {
            Debug.Log("HAS FOOD");
            CustomerManager.main.ClearTable(table);
        //leave
    }

}
