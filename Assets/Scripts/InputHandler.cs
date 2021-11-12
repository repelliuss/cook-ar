using UnityEngine;
using Vuforia;

public class InputHandler : MonoBehaviour
{
    void Awake()
    {
        VuforiaApplication.Instance.OnVuforiaInitialized += IgnoreRaycastsOnEmulatorGroundPlane;
    }

    void IgnoreRaycastsOnEmulatorGroundPlane(VuforiaInitError error)
    {
        if (error == VuforiaInitError.NONE)
        {
            int ignored = LayerMask.NameToLayer("Ignore Raycast");
            GameObject plane = GameObject.Find("/emulator_ground_plane");

            if (!plane || ignored == -1) {
                Debug.LogWarning("Couldn't ignore emulator ground plane!");
                return;
            }

            plane.layer = ignored; // ignore

            // normally there is only 1 child
            foreach (Transform child in plane.transform)
            {
                child.gameObject.layer = ignored;
            }
        }

    }

    void handleTap(Vector2 position)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Button button = hit.transform.GetComponent<Button>();
            if(button)
            {
                button.Press();
            }
        }
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                handleTap(touch.position);
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            handleTap(Input.mousePosition);
        }
#endif
    }
}
