using UnityEngine;

public class VerdwijnScript : MonoBehaviour
{
    public string triggerTag = "Key";

    public GameObject objectToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
        {
            // Zet dit object uit
            gameObject.SetActive(false);

            // Activeer het andere object als het is toegewezen
            // if (objectToActivate != null)
            // {
            //     objectToActivate.SetActive(true);
            // }
        }
    }
}
