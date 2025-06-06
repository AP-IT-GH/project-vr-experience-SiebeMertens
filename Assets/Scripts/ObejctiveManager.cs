using UnityEngine;

public class ObejctiveManager : MonoBehaviour
{
    public GameObject lockIncomplete;
    public GameObject lockComplete;

    public GameObject palletIncomplete;
    public GameObject palletComplete;

    public Animator leftDoorAnimator;
    public Animator rightDoorAnimator;

    private bool doorOpened = false;

    void Update()
    {
        if (!doorOpened &&
            !lockIncomplete.activeSelf &&
            lockComplete.activeSelf &&
            !palletIncomplete.activeSelf &&
            palletComplete.activeSelf)
        {
            OpenDoors();
        }
    }

    void OpenDoors()
    {
        leftDoorAnimator.SetTrigger("OpenDoors");
        rightDoorAnimator.SetTrigger("OpenDoors");
        doorOpened = true;
        Debug.Log("Both doors opened: objectives completed.");
    }

}
