using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Assertions;

public enum Location { FRIDGE, STAND, PAN };

public class SteakLogic : MonoBehaviour
{
    int[] mealStates = new int[2];
    public bool Failed = false;
    public RectTransform resetButton;
    public RectTransform doneButton;
    public Timer timer;
    public VideoPlayer video;

    Location _location = Location.FRIDGE;
    public Location SteakLocation
    { get => _location; set => _location = value; }

    private Animator catAnimator;

    void Start()
    {
        Assert.IsNotNull(resetButton, "Assign a game reset button to " + name);
        Assert.IsNotNull(doneButton, "Assign a game done button to " + name);
        Assert.IsNotNull(video, "Assign a video player to " + name);
        catAnimator = GameObject.FindGameObjectWithTag("Movable").GetComponent<Animator>();
    }

    public void Fail()
    {
        Failed = true;
        Debug.Log("FAILED...");
        RectTransform t = resetButton;
        t.anchorMin = t.anchorMax = t.pivot = new Vector2(0.5f, 0.5f);
        t.localScale *= 2;
        video.Pause();
        catAnimator.enabled = false;
    }

    public void Done()
    {
        Debug.Log("DONE!!!");
        resetButton.gameObject.SetActive(false);
        doneButton.gameObject.SetActive(true);
    }

    public void CookingStart()
    {
        Debug.Log("Cooking begins!");
        timer.AlarmInSeconds = 10;
        timer.StartTimer();
    }

    public void AddSpice()
    {
        if(SteakLocation == Location.STAND)
        {
            ++mealStates[0];
        }
    }

    public void OpenFire()
    {
        if(mealStates[1] == 0)
            ++mealStates[1];
    }

    public void AddOil()
    {
        if(IsPanHot())
            ++mealStates[1];
        else
            Fail();
    }

    public bool IsSpicingDone() => mealStates[0] > 1;
    public bool IsPanReady() => mealStates[1] > 1;
    public bool IsPanHot() => mealStates[1] > 0;
}
