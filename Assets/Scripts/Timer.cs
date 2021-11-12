using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class Timer : MonoBehaviour
{
    public int AlarmInSeconds
        { get; set; }

    [SerializeField]
    private Text text;

    [SerializeField]
    private SteakLogic logic;

    private bool alarmed = true;
    private float remainingTime = -1;
    private float lastSecond;

    void Start()
    {
        Assert.IsNotNull(logic, "Assign a steak logic to " + name);
        Assert.IsNotNull(text, "Assign a text to " + name);
    }

    public void StartTimer()
    {
        lastSecond = remainingTime = AlarmInSeconds;
        alarmed = false;
        text.gameObject.SetActive(true);
        DisplayTime(lastSecond);
    }

    void Update()
    {
        remainingTime -= Time.deltaTime;

        if(remainingTime > 0.0f)
        {
            if(lastSecond - remainingTime > 1)
            {
                SecondCallback();
            }
        }
        else if(!alarmed) {
            alarmed = true;
            SecondCallback();
            Alarm();
        }
    }

    void Alarm()
    {
        logic.Done();
    }

    void SecondCallback()
    {
        lastSecond -= 1;
        DisplayTime(lastSecond);
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        text.text = string.Format("{0:00}:{1:00}", minutes, seconds - 1);
    }
}
