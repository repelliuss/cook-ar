using System.IO;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Assertions;

public class VideoActions : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer video;

    void Start()
    {
        Assert.IsNotNull(video, "Assign a video to " + name);
#if UNITY_EDITOR
        video.url = Path.Combine(Application.streamingAssetsPath, "GordonHowtoVP8.webm");
#elif UNITY_ANDROID
        video.url = Path.Combine(Application.streamingAssetsPath, "GordonHowtoVP9.webm");
#endif
    }

    public void PlayVideo()
    {
        video.Play();
    }
}
