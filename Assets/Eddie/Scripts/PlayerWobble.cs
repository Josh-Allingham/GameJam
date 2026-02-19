using UnityEngine;

public class PlayerWobble : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private bool hasFallen;
    [SerializeField] private GameObject balancePointer;
    public float clickScale = 1.2f;
    private Vector2 minMaxRotInDegrees = new Vector2(-45f, 45f);
    

    [Header("Noise")]
    public float noiseMultiplier = 1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        hasFallen = CheckIfFallen();
        if (hasFallen)
        {
            if (Random.value < 0.02) Debug.Log((new string[] { "OUCH", "AHH", "OW" })[Random.Range(0, 3)]);
        }
        else
        {
            float noiseZ = Mathf.PerlinNoise(Time.time, balancePointer.transform.localEulerAngles.z);
            
            float sinZ = Mathf.Sin(noiseZ * 2 * Mathf.PI);
            
            float clickMultiplier = 1;
            clickMultiplier *= Input.GetMouseButton(0) ? clickScale : 1;
            clickMultiplier *= Input.GetMouseButton(1) ? -clickScale : 1;
            
            //Vector3 newRot = new Vector3(0, 0, angleX * minMaxRotInDegrees.y);
            
            Vector3 newRot = balancePointer.transform.localEulerAngles + Vector3.forward * sinZ * clickMultiplier;
            //Debug.Log(balancePointer.transform.localEulerAngles + " " + newRot);
            //balancePointer.transform.localEulerAngles = newRot;
            balancePointer.transform.localEulerAngles += Vector3.forward * sinZ * noiseMultiplier * Time.deltaTime;
            balancePointer.transform.localEulerAngles += Vector3.forward * clickMultiplier * Time.deltaTime;



        }


    }

    bool CheckIfFallen()
    {
        return !(balancePointer.transform.localEulerAngles.z <= minMaxRotInDegrees.y || balancePointer.transform.localEulerAngles.z >= 360 + minMaxRotInDegrees.x);
        
    }
}
