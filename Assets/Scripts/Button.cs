using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent onPressed;

    public void Press()
    {
        onPressed.Invoke();
    }
}
