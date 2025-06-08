using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadWinScene : MonoBehaviour
{
    public string targetTag = "Target";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.Log("Target entered trigger");
            SceneManager.LoadScene("WinOutroScene");
        }
    }
}
