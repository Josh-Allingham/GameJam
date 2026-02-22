using UnityEngine;
using TMPro;
public class DishObject : MonoBehaviour
{

    private bool isPickedUp = false;
    private Transform followTarget = null;
    [SerializeField] private TMP_Text dishText;
    public static Vector3 dishSpawnPos = new Vector3(9.75f, 3.5f, 20f);
    public int tableIndex;
    public int plateIndex;
    public string name;
    
    public DishObject(int tableIndex, int plateIndex, string name)
    {
        this.tableIndex = tableIndex;
        this.plateIndex = plateIndex;
        this.name = name;
    }

    public void ImportSettings(string _name, int _tableNumber, int _seatNumber)
    {
        this.name = _name;
        this.tableIndex = _tableNumber - 1;
        this.plateIndex = _seatNumber - 1;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
        dishText.text = $"{name} \n Table {tableIndex + 1}";
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickedUp && followTarget != null)
        {
            transform.position = followTarget.position;
        }
    }

    public void GetPickedUp(Transform player)
    {
        isPickedUp = true;
        followTarget = player;

    }

    public void GetPlacedAt(Vector3 position)
    {
        transform.position = position;
        isPickedUp = false;
        dishText.text = "";
    }
}
