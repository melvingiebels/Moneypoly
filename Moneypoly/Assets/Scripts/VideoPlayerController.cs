using System;
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
    public AudioManager audioManager;
    private NewsFlashImporter newsFlashImporter;

    private List<NewsFlash> newsFlashes;
    private NewsFlash newsFlash;
    void Start()
    {
        rawImage.enabled = true;
        CloseNews();
        videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
        videoPlayer.SetDirectAudioMute(0, true);

        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Nostechno");
        //lower the volume of the music
        

        videoPlayer.loopPointReached += OnVideoEnd;

        newsFlashImporter = GameObject.FindObjectOfType<NewsFlashImporter>();
        if (newsFlashImporter != null)
        {
            // Use the newsFlashImporter object
            newsFlashImporter.ImportNewsFlashes(); // Import the news flashess
            SetNewsFlash();
            Debug.Log("NewsFlashImporter object found");
        }
        else
        {
            // NewsFlashImporter object not found
            Debug.LogWarning("NewsFlashImporter object not found");
        }
        

        

        continueButton.onClick.AddListener(CloseScreen);
       
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
        continueButton.enabled = true;
    }

    public void CloseScreen()
    {
        // Close the whole screen by disabling the parent GameObject
        transform.parent?.gameObject.SetActive(false);

        // Stop the "Nostechno" coroutine from the AudioManager (assuming it's implemented as a coroutine)
        audioManager?.StopMusic("Nostechno");

        // Close the news elements
        CloseNews();

        BackToGame();

        // Destroy this object
        Destroy(gameObject);
    }
    public void BackToGame()
    {
        BoardController boardController = FindObjectOfType<BoardController>();
        boardController?.OpenGame(newsFlash);
    }
    public void SetNewsFlash(NewsFlash newsFlash)
    {
        newsFlashText.text = newsFlash.text;
        newsFlashSubject.text = newsFlash.subject;
    }
    public void CloseNews()
    {
        imageToShow.enabled = false;
        newsFlashText.enabled = false;
        newsFlashSubject.enabled = false;
        continueButton.enabled = false;
    }
    
    public void SetNewsFlash()
    {
        newsFlashes = newsFlashImporter.GetNewsFlashes();

        if (newsFlashes != null && newsFlashes.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, newsFlashes.Count);
            this.newsFlash = newsFlashes[randomIndex];
            newsFlashText.text = newsFlash.text;
            newsFlashSubject.text = newsFlash.subject;
        }
        else
        {
            Debug.LogWarning("No news flashes available. Please import data from CSV first.");
        }
    }


}
