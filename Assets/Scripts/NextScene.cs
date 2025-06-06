using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // [SerializeField] private string nextSceneName = "TestWithCharacter"; // Name of the next scene to load

    public void StartGame()
    {
        // Load scene by name
        SceneManager.LoadScene("TestWithCharacter");
    }
    public void QuitGame()
    {
        Debug.Log("Game stopped");
        Application.Quit();
    }
}
