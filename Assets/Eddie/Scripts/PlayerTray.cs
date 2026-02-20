using UnityEngine;
using System.Collections.Generic;
public class PlayerTray : MonoBehaviour
{
    public static PlayerTray main;

    public List<Table> allTables = new List<Table>();
    public List<DishObject> dishesToDeliver = new List<DishObject>();
    private DishObject currentDish;
    [SerializeField] public DishObject dishPrefab;
    [SerializeField] private float tableDistance = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        #region Create all tables
        allTables.Add(new Table(0, null, new Vector3[] { new Vector3(16.41f, 3.52f, -0.16f), new Vector3(16.16f, 3.52f, -1.79f) }, new Vector3(15.6f, 3.65f, -0.37f)));
        allTables.Add(new Table(1, null, new Vector3[] { new Vector3(7.33f, 3.52f, -5.29f), new Vector3(7.33f, 3.52f, -7.74f), new Vector3(8.48f, 3.52f, -6.27f) }, new Vector3(7.13f, 3.65f, -6.37f)));
        allTables.Add(new Table(2, null, new Vector3[] { new Vector3(16.22f, 3.52f, -12.22f), new Vector3(16.87f, 3.52f, -13.94f), new Vector3(15.24f, 3.52f, -14.65f) }, new Vector3(15.88f, 3.65f, -13.49f)));
        allTables.Add(new Table(3, null, new Vector3[] { new Vector3(-15.84f, 3.52f, -16.63f), new Vector3(-15.84f, 3.52f, -14.18f) }, new Vector3(-15.87f, 3.65f, -15.2f)));
        allTables.Add(new Table(4, null, new Vector3[] { new Vector3(-11.89f, 3.52f, -7.18f), new Vector3(-11.89f, 3.52f, -4.66f), new Vector3(-10.64f, 3.52f, -6.178f), new Vector3(-13.27f, 3.52f, -6.178f) }, new Vector3(-11.82f, 3.65f, -6.09f)));
        allTables.Add(new Table(5, null, new Vector3[] { new Vector3(-15.81f, 3.52f, 3.46f), new Vector3(-15.81f, 3.52f, 5.97f) }, new Vector3(-15.98f, 3.65f, 4.72f)));
        #endregion

        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDish != null && Input.GetKeyDown(KeyCode.E))
        {
            PickUpDish(currentDish);
            currentDish.GetComponent<Collider>().enabled = false;
            currentDish = null;
        }

        foreach (Table table in allTables)
        {
            if (Vector3.Distance(transform.position, table.tablePosition) < tableDistance && Input.GetKeyDown(KeyCode.E))
            {
                PlaceDishAtTable(table.tableIndex);
            }
        }
    }

    void PlaceDishAtTable(int tableIndex)
    {
        foreach (DishObject dish in dishesToDeliver)
        {
            //check if we have a dish for this table

            if (dish.tableIndex == tableIndex)
            {
                //get dish location
                Vector3 dishLocation = allTables[tableIndex].dishPositions[dish.plateIndex];

                //place corresponding dish
                dish.GetPlacedAt(dishLocation);
                //remove 
                dishesToDeliver.Remove(dish);
                //allTables[tableIndex].wantedDishes.Remove(dish);

                CustomerManager.main.GiveFood(tableIndex);



            }
        }
    }

    void PickUpDish(DishObject dish)
    {
        dishesToDeliver.Add(dish);
        dish.GetPickedUp(transform);       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DishObject dish))
        {
            currentDish = dish;
        }
    }

  
    public struct Table
    {
        public int tableIndex;
        public List<DishObject> wantedDishes;
        public Vector3[] dishPositions;
        public int dishSlots;
        public Vector3 tablePosition;
        public bool hasFood;

        public Table(int tableIndex, List<DishObject> wantedDishes, Vector3[] dishPositions, Vector3 tablePosition)
        {
            this.tableIndex = tableIndex;
            this.wantedDishes = wantedDishes;
            this.dishPositions = dishPositions;
            this.tablePosition = tablePosition;
            this.dishSlots = dishPositions.Length;
            this.hasFood = false;
        }

        public DishObject GetDish(int dishIndex)
        {
            try
            {
                return wantedDishes[dishIndex];
            }
            catch
            {
                Debug.Log("h");
            }
            return new DishObject(0, 0, "missingno");
        }
    }
}
