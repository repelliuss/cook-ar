using UnityEngine;
using Vuforia;

public class OvenAnimationTriggerer : MonoBehaviour
{
    public Animator animator;
    public SteakLogic logic;

    VirtualButtonBehaviour vrb;

    void Start()
    {
        vrb = GetComponent<VirtualButtonBehaviour>();

        vrb.RegisterOnButtonPressed(OnPress);
    }

    public void OnPress(VirtualButtonBehaviour vb)
    {
        logic.OpenFire();
        animator.SetTrigger("open");
    }
}
