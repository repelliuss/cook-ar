using UnityEngine;
using UnityEngine.Assertions;

public class SpiceTriggerAnimation : MonoBehaviour
{
    public Animator anim;
    public string trigger;
    public SteakLogic logic;

    void Start()
    {
        Assert.AreNotEqual(trigger.Length, 0,
                           "No animation trigger given to " + name);
        Assert.IsNotNull(anim, "No animator given to " + name);
        logic = GameObject.FindGameObjectWithTag("Recipe")
            .GetComponent<SteakLogic>();
    }

    public void Trigger()
    {
        if(!logic.Failed && logic.SteakLocation == Location.STAND)
            anim.SetTrigger(trigger);
    }
}
