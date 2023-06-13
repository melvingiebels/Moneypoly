using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameButtonScript : MonoBehaviour
{
    public List<PlayerSelectTab> playerSelectedScreens;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.UnloadSceneAsync("PlayGameScene");
        SceneManager.LoadScene("MauriceScene");
        Scene loadedScene = SceneManager.GetSceneByName("MauriceScene");
        float yChord = -164f;
        foreach (PlayerSelectTab playerSelectTab in playerSelectedScreens)
        {
            GameObject player = new GameObject("Player" + playerSelectTab.PlayerNameText);
            player.transform.SetParent(loadedScene.GetRootGameObjects()[0].transform); // Attach to the root object of the scene
            PlayerController playerController = player.AddComponent<PlayerController>();
            playerController.Initialize(playerSelectTab.PlayerImage.sprite, yChord++); // Pass player-specific parameters
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.UnloadSceneAsync("PlayGameScene");
        SceneManager.LoadScene("MainMenuScene");
    }
}
