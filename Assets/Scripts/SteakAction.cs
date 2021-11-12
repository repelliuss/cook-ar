using UnityEngine;

public class SteakAction : MonoBehaviour
{
    Animator anim;
    SteakLogic logic;

    void Start()
    {
        anim = GetComponent<Animator>();
        logic = GameObject.FindGameObjectWithTag("Recipe")
            .GetComponent<SteakLogic>();
    }

    public void OnPressed()
    {
        switch (logic.SteakLocation)
        {
            case Location.FRIDGE:
                anim.SetTrigger("toStand");
                logic.SteakLocation = Location.STAND;
                break;
            case Location.STAND:
                if (logic.IsPanReady() && logic.IsSpicingDone())
                {
                    anim.SetTrigger("toPan");
                    logic.SteakLocation = Location.PAN;
                    logic.CookingStart();
                }
                else
                {
                    logic.Fail();
                }
                break;
            default:
                break;
        }
    }
}
