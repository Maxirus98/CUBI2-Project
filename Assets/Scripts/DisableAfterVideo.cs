using UnityEngine;
using UnityEngine.Video;

public class DisableAfterVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    [SerializeField] public GameObject endMenu;
    [SerializeField] public GameObject endState;
    public RenderTexture blankTexture;

    void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
        videoPlayer.started += VideoPlayer_started;
        videoPlayer.loopPointReached += EndReached;
    }

    private void VideoPlayer_started(VideoPlayer source)
    {
        endMenu.SetActive(false);
        endState.SetActive(false);
    }

    void EndReached(VideoPlayer vp)
    {
        vp.targetTexture.Release();
        vp.targetTexture = blankTexture;
        endMenu.SetActive(true);
        endState.SetActive(true);
    }
}
