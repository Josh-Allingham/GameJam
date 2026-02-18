using UnityEngine;

public class DishObject : MonoBehaviour
{

    private bool isPickedUp = false;
    private Transform followTarget = null;

    public int tableIndex;
    public int plateIndex;
    public string name;
    
    public DishObject(int tableIndex, int plateIndex, string name)
    {
        this.tableIndex = tableIndex;
        this.plateIndex = plateIndex;
        this.name = name;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPickedUp && followTarget != null)
        {
            transform.position = followTarget.position + Vector3.up * (1 + followTarget.GetComponent<PlayerTray>().dishesToDeliver.Count);
        }
        
    }

    public void GetPickedUp(Transform player)
    {
        isPickedUp = true;
        followTarget = player;
    }

    public void GetPlaced()
    {
        isPickedUp = false;
    }
}
