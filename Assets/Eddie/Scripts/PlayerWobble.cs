using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerWobble : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] private GameObject balancePointer;
    public float clickScale = 1.2f;
    private Vector2 minMaxRotInDegrees = new Vector2(-90f, 90f);
    private Animator anim;

    [Header("Noise")]
    public float noiseMultiplier = 1f;

    // Physics-like state and tuning
    [Header("Physics Tuning")]
    [SerializeField] private float gravityFactor = 8f; // acceleration gain from current angle
    [SerializeField] private float damping = 4f; // angular damping (higher = faster stabilization)
    [SerializeField] private float inputStrength = 60f; // strength of mouse input torque
    [SerializeField] private float maxAngularVelocity = 360f; // deg/sec cap
    [SerializeField] private float noiseFrequency = 0.5f; // frequency multiplier for Perlin noise

    private float angle; // internal angle in degrees (-180..180)
    private float angularVelocity; // deg/sec

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        angle = NormalizeAngle(balancePointer.transform.localEulerAngles.z);
        angularVelocity = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        switch (CheckIfFalling())
        {
            case true:
                anim.SetBool("Falling", true);
                anim.SetBool("Stable", false);
                GetComponent<PlayerControler>().canMove = false;
                break;
            case false:
                anim.SetBool("Stable", true);
                anim.SetBool("Falling", false);
                GetComponent<PlayerControler>().canMove = true;
                break;
        }
        switch (CheckIfFallen())
        {
            case true:
                anim.SetBool("Fallen", true);
                anim.SetBool("Falling", false);
                StartCoroutine("FallenOver");
                break;
            case false:
                MovePointer();
                break;
        }

    }

    void MovePointer()
    {
        float dt = Time.deltaTime;

        // 1) Noise torque: smooth Perlin noise in range [-1,1]
        float noiseSample = Mathf.PerlinNoise(Time.time * noiseFrequency, 0f) * 2f - 1f;
        float noiseTorque = noiseSample * noiseMultiplier;

        // 2) Gravity-like unstable torque: accelerates the further away from center the pointer is.
        //    Using angle directly makes acceleration approximately linear with tilt.
        float gravityTorque = angle * (gravityFactor * Mathf.Deg2Rad); // convert to a sensible scale

        // 3) Input torque: left mouse -> negative torque, right mouse -> positive torque
        float inputTorque = 0f;
        if (Input.GetMouseButton(1)) inputTorque -= inputStrength;
        if (Input.GetMouseButton(0)) inputTorque += inputStrength;

        // 4) Integrate angular acceleration -> angular velocity
        //    Treat (gravityTorque + inputTorque + noiseTorque) as acceleration in deg/sec^2 (scaled).
        float totalTorque = gravityTorque + inputTorque + noiseTorque;
        angularVelocity += totalTorque * dt;

        // 5) Apply damping (viscous)
        angularVelocity = Mathf.MoveTowards(angularVelocity, 0f, damping * dt * Mathf.Abs(angularVelocity));

        // 6) Clamp angular velocity
        angularVelocity = Mathf.Clamp(angularVelocity, -maxAngularVelocity, maxAngularVelocity);

        // 7) Integrate angle
        angle += angularVelocity * dt;

        // 8) Clamp angle to configured limits and zero velocity if hit.
        float minAngle = minMaxRotInDegrees.x;
        float maxAngle = minMaxRotInDegrees.y;
        if (angle < minAngle)
        {
            angle = minAngle;
            angularVelocity = 0f;
        }
        else if (angle > maxAngle)
        {
            angle = maxAngle;
            angularVelocity = 0f;
        }

        // 9) Apply angle to transform (convert to 0..360 for localEulerAngles)
        Vector3 euler = balancePointer.transform.localEulerAngles;
        euler.z = (angle < 0f) ? (360f + angle) : angle;
        balancePointer.transform.localEulerAngles = euler;
    }

    IEnumerator FallenOver()
    {
        GetComponent<PlayerControler>().canMove = false;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }

    bool CheckIfFalling()
    {
        // Falling if outside small stable window (+/-19 degrees)
        return Mathf.Abs(angle) > 19f;
    }

    public bool CheckIfFallen()
    {
        // Fallen if beyond configured min/max absolute limit
        float limit = Mathf.Max(Mathf.Abs(minMaxRotInDegrees.x), Mathf.Abs(minMaxRotInDegrees.y));
        return Mathf.Abs(angle) >= limit;
    }

    private float NormalizeAngle(float z)
    {
        float a = z;
        if (a > 180f) a -= 360f;
        return a;
    }
}
