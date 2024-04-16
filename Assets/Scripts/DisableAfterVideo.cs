using UnityEngine;
using UnityEngine.Video;

public class DisableAfterVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    [SerializeField] public GameObject endMenu;
    [SerializeField] public GameObject endState;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }

        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        //gameObject.SetActive(false);
        endMenu.SetActive(true);
        endState.SetActive(true);
    }
}
