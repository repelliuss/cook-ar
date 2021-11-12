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

    private new AudioSource audio;

    [SerializeField]
    private AudioClip audioClipFail;
    [SerializeField]
    private AudioClip audioClipDone;

    [SerializeField]
    private GameObject cookedSteak;
    [SerializeField]
    private GameObject uncookedSteak;

    void Start()
    {
        Assert.IsNotNull(resetButton, "Assign a game reset button to " + name);
        Assert.IsNotNull(doneButton, "Assign a game done button to " + name);
        Assert.IsNotNull(video, "Assign a video player to " + name);
        Assert.IsNotNull(audioClipFail, "Assign a fail audio clip to " + name);
        Assert.IsNotNull(audioClipDone, "Assign a done audio clip to " + name);
        Assert.IsNotNull(cookedSteak, "Assign a cooked steak object to " + name);
        Assert.IsNotNull(uncookedSteak, "Assign a uncooked steak object to " + name);
        catAnimator = GameObject.FindGameObjectWithTag("Movable").GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    public void Fail()
    {
        Failed = true;
        RectTransform t = resetButton;
        t.anchorMin = t.anchorMax = t.pivot = new Vector2(0.5f, 0.5f);
        t.localScale *= 2;
        video.Pause();
        catAnimator.enabled = false;
        audio.clip = audioClipFail;
        audio.Play();
    }

    public void Done()
    {
        resetButton.gameObject.SetActive(false);
        doneButton.gameObject.SetActive(true);
        uncookedSteak.gameObject.SetActive(false);
        cookedSteak.gameObject.SetActive(true);
        audio.clip = audioClipDone;
        audio.Play();
        video.SetDirectAudioMute(0, true);
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
