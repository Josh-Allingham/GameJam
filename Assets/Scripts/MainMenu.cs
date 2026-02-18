using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject mainMenuUI;

    [SerializeField]
    public GameObject settingsMenuUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainMenuUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playGame()
    {

    }

    public void settingsMenu()
    {
        mainMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
