using UnityEngine;

public class DoctorLaugh : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] laughClips;
    public float minInterval = 10f;
    public float maxInterval = 20f;

    void Start()
    {
        StartCoroutine(PlayRandomLaughs());
    }

    System.Collections.IEnumerator PlayRandomLaughs()
    {
        while (true)
        {
            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);

            if (!audioSource.isPlaying)
            {
                int index = Random.Range(0, laughClips.Length);
                audioSource.clip = laughClips[index];
                audioSource.Play();
            }
        }
    }
}
