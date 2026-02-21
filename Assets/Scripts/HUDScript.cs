using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public static HUDScript main;

    [SerializeField]
    public Image bossFace;
    [SerializeField]
    public Image moneyJar;
    [SerializeField]
    public TMP_Text moneyTxt;

    float angerLvl = 0f;
    int moneyValue = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        main = this;    
    }

    // Update is called once per frame
    void Update()
    {
        updateJar();
        updateAnger();
        checkAnger();
        bossFace.fillAmount = angerLvl;
        moneyTxt.text = moneyValue.ToString();
    }

    public void updateAnger()
    {
        angerLvl += Time.deltaTime * 0.05f;
    }

    public void updateJar()
    {
        if(moneyValue > 0)
        {
            moneyJar.fillAmount = moneyValue / 100f;
        }
    }

    public void tableServed()
    {
        moneyValue += 15;
        angerLvl = 0f; 
    }

    public void platesDropped()
    {
        moneyValue -= 5;
        angerLvl += 0.1f;
    }

    public void checkAnger()
    {
        if(angerLvl >= 1f)
        {
            GlobalSettings.score = moneyValue;
            SceneManager.LoadScene("GameOver");
        }
    }
}
