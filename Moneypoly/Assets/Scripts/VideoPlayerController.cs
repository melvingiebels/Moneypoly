using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public Image imageToShow;

    void Start()
    {
        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;

        // Mute the audio track of the video
        videoPlayer.SetDirectAudioMute(0, true);

        FindObjectOfType<AudioManager>().Play("Nostechno");

        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        rawImage.enabled = false;
        imageToShow.enabled = true;
    }
}
