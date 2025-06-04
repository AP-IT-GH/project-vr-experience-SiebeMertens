using UnityEngine;

public class PlayDoorSounds : MonoBehaviour
{    public AudioSource audioSource;
    public AudioClip doorOpenSound;

    public void PlayDoorSound()
    {
        audioSource.PlayOneShot(doorOpenSound);
    }
}
