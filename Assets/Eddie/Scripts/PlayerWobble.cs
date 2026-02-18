using UnityEngine;

public class PlayerWobble : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private bool hasFallen;
    private Vector2 minMaxRotInDegrees = new Vector2(-45f, 45f);
    

    [Header("Noise")]
    public float noiseScale = 1f;

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
            float noiseX = Mathf.PerlinNoise(Time.time * noiseScale, transform.rotation.x);
            float noiseZ = Mathf.PerlinNoise(Time.time * noiseScale, transform.rotation.z);

            float angleX = Mathf.Sin(noiseX * 2 * Mathf.PI);
            float angleZ = Mathf.Sin(noiseZ * 2 * Mathf.PI);
            //Debug.Log($"{noiseX} {noiseZ} {angleX} {angleZ}");
            transform.Rotate(Vector3.right, angleX);
            transform.Rotate(Vector3.forward, angleZ);

            //transform.rotation = Quaternion.Euler(angleX * minMaxRotInDegrees.y, transform.rotation.y, angleZ * minMaxRotInDegrees.y);
        }


    }

    bool CheckIfFallen()
    {
        return Vector3.Angle(Vector3.up, transform.up) > minMaxRotInDegrees.y;
    }
}
