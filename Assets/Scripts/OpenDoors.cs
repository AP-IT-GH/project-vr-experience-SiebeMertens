using UnityEngine;
public class OpenDoors : MonoBehaviour
{
    public Animator doorAnimator;

    public bool padlockSolved = false;
    public bool palletDestroyed = false;
    public bool hasKey = false;

    private bool Opened = false;

    void Update()
    {
        if (padlockSolved && palletDestroyed && hasKey && !Opened)
        {
            doorAnimator.SetBool("OpenDoors", true);
            Opened = true;
        }
    }

    // Optional methods for other scripts to call
    public void SolvePadlock() => padlockSolved = true;
    public void DestroyPallet() => palletDestroyed = true;
    public void GetKey() => hasKey = true;
}

