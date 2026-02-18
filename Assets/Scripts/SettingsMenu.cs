using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    public GameObject previousMenu;

    [SerializeField]
    public GameObject settingsMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void backToMainMenu()
    {
        settingsMenu.SetActive(false);
        previousMenu.SetActive(true);
    }


}
