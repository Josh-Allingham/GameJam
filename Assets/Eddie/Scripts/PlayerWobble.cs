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

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (CheckIfFalling())
        {
            anim.SetBool("Falling", true);
            anim.SetBool("Stable", false);
        }
        if (CheckIfFallen())
        {
            anim.SetBool("Fallen", true);
            anim.SetBool("Falling", false);
            StartCoroutine("FallenOver");
        }
        if (!CheckIfFallen())
        {
            float noiseZ = Mathf.PerlinNoise(Time.time, balancePointer.transform.localEulerAngles.z);

            float adjustedNoise = noiseZ * 2 - 1;

            float clickMultiplier = 1;
            clickMultiplier *= Input.GetMouseButton(0) ? clickScale : 1;
            clickMultiplier *= Input.GetMouseButton(1) ? -clickScale : 1;

            balancePointer.transform.localEulerAngles += Vector3.forward * adjustedNoise * noiseMultiplier * Time.deltaTime;
            balancePointer.transform.localEulerAngles += Vector3.forward * clickMultiplier * Time.deltaTime;
            
        }
        


    }

    IEnumerator FallenOver()
    {
        GetComponent<PlayerControler>().canMove = false;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }
    bool CheckIfFalling()
    {
        return !(balancePointer.transform.localEulerAngles.z <= minMaxRotInDegrees.y/2 || balancePointer.transform.localEulerAngles.z >= 360 + minMaxRotInDegrees.x/2);
    }

    public bool CheckIfFallen()
    {
        return !(balancePointer.transform.localEulerAngles.z <= minMaxRotInDegrees.y || balancePointer.transform.localEulerAngles.z >= 360 + minMaxRotInDegrees.x);
        
    }
}
