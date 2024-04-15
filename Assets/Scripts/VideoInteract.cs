using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoInteract : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject intro;
    public Button skipButton, tutosButton, closeTutosButton;
    public GameObject tutos;

    void Start()
    {

        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>();
        }
        skipButton.onClick.AddListener(skipTutos);
        tutosButton.onClick.AddListener(showTutos);
        closeTutosButton.onClick.AddListener(closeTutos);
        intro.SetActive(false);
        tutos.SetActive(false);
        videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        intro.SetActive(true);
    }

    public void skipTutos()
    {
        intro.SetActive(false);
        videoPlayer.gameObject.SetActive(false);
    }

    public void showTutos()
    {
        intro.SetActive(false);
        videoPlayer.gameObject.SetActive(false);
        tutos.SetActive(true);
    }

    public void closeTutos()
    {
        tutos.SetActive(false);
    }
}
