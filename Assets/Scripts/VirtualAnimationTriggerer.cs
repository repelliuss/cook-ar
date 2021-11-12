using UnityEngine;
using Vuforia;

public class VirtualAnimationTriggerer : MonoBehaviour
{
    public Animator animator;
    public string trigger;

    VirtualButtonBehaviour vrb;

    void Start()
    {
        vrb = GetComponent<VirtualButtonBehaviour>();

        vrb.RegisterOnButtonPressed(OnPress);
    }

    public void OnPress(VirtualButtonBehaviour vb)
    {
        animator.SetTrigger(trigger);
    }

}
