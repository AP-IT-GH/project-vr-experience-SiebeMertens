using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Laadt de volgende scene op basis van Build Index + 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // OF als je een specifieke naam wil gebruiken:
        // SceneManager.LoadScene("NaamVanDeVolgendeScene");
    }
}
