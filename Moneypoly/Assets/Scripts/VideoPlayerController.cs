using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RawImage rawImage;
    public Image imageToShow;
    public TextMeshProUGUI newsFlashText;
    public Button continueButton;
    void Start()
    {
        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;

        // Mute the audio track of the video
        videoPlayer.SetDirectAudioMute(0, true);

        FindObjectOfType<AudioManager>().Play("Nostechno");

        videoPlayer.loopPointReached += OnVideoEnd;

        //Click for next scene 
        
        continueButton.onClick.AddListener(SwitchScene);

    }
    public void SetNewsText(string text)
    {
        newsFlashText.text = text;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        rawImage.enabled = false;
        imageToShow.enabled = true;
        newsFlashText.enabled = true;
    }

    public void SwitchScene()
    {
        //load the next scene 
        SceneController.instance.LoadScene("StockScene");

    }
}
