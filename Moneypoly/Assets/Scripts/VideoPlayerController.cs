using System.Collections.Generic;
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
    public TextMeshProUGUI newsFlashSubject;
    public Button continueButton;
    private AudioManager audioManager;
    private NewsFlashImporter newsFlashImporter= new NewsFlashImporter();

    private List<NewsFlash> newsFlashes;
    void Start()
    {
        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
        videoPlayer.SetDirectAudioMute(0, true);

        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Nostechno");

        videoPlayer.loopPointReached += OnVideoEnd;

        newsFlashImporter = FindObjectOfType<NewsFlashImporter>();
        newsFlashImporter.ImportNewsFlashes(); // Import the news flashes

        SetNewsFlash();

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
        newsFlashSubject.enabled = true;
    }

    public void SwitchScene()
    {
        //stop the music
        audioManager.StopAllCoroutines();
        //load the next scene 
        SceneController.instance.LoadScene("StockScene");

    }
    public void SetNewsFlash(NewsFlash newsFlash)
    {
        newsFlashText.text = newsFlash.text;
        newsFlashSubject.text = newsFlash.subject;
    }
    public void SetNewsFlash()
    {
        newsFlashes = newsFlashImporter.GetNewsFlashes();

        if (newsFlashes != null && newsFlashes.Count > 0)
        {
            int randomIndex = Random.Range(0, newsFlashes.Count);
            NewsFlash newsFlash = newsFlashes[randomIndex];
            newsFlashText.text = newsFlash.text;
            newsFlashSubject.text = newsFlash.subject;
        }
        else
        {
            Debug.LogWarning("No news flashes available. Please import data from CSV first.");
        }
    }


}
