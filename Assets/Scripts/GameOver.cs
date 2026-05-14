using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TMP_Text scoreText;

    void Start()
    {
        Debug.Log(GlobalSettings.score);
        Debug.Log(GlobalSettings.score.ToString());
        scoreText.text = "$ " + GlobalSettings.score.ToString();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Update()
    {
        Debug.Log(GlobalSettings.score);
        Debug.Log(GlobalSettings.score.ToString());
    }

    public void restartGame()
    {
        SceneManager.LoadScene("GameScene");
        GlobalSettings.score = 0;
    }

    public void quitGame()
    {
        SceneManager.LoadScene("MainMenu");
        GlobalSettings.score = 0;
    }

}
