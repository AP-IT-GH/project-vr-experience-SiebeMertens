// Script by Marcelli Michele

using System.Linq;
using UnityEngine;

public class PadLockPassword : MonoBehaviour
{
    MoveRuller _moveRull;

    public int[] _numberPassword = { 0, 0, 0, 0 };
    public GameObject padlockObjectToHide;
    public GameObject unlockedPadlock;

    private void Awake()
    {
        _moveRull = FindObjectOfType<MoveRuller>();
    }

    public void Password()
    {
        if (_moveRull._numberArray.SequenceEqual(_numberPassword))
        {
            // Here enter the event for the correct combination
            Debug.Log("Password correct");
            // Deactiveer padlock object
            if (padlockObjectToHide != null)
            {
                padlockObjectToHide.SetActive(false);
            }
            else
            {
                // Als je dit script op het object zelf hebt staan dat moet verdwijnen
                gameObject.SetActive(false);
            }
            if (unlockedPadlock != null)
            {
                unlockedPadlock.SetActive(true);

                // Optioneel: zet hem op de grond (pas de vector aan indien nodig)
                unlockedPadlock.transform.position = new Vector3(
                    unlockedPadlock.transform.position.x,
                    0f, // of de gewenste y-positie op de grond
                    unlockedPadlock.transform.position.z
                );
            }

            // Es. Below the for loop to disable Blinking Material after the correct password
            for (int i = 0; i < _moveRull._rullers.Count; i++)
            {
                _moveRull._rullers[i].GetComponent<PadLockEmissionColor>()._isSelect = false;
                _moveRull._rullers[i].GetComponent<PadLockEmissionColor>().BlinkingMaterial();
            }

        }
    }
}
