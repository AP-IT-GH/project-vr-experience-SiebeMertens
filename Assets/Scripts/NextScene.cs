
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("testWithCharacter");

    }

    public void QuitGame()
    {
        Debug.Log("Game stopped");
        Application.Quit();

    }
}
