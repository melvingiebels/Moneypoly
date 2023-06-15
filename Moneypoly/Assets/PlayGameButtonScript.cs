using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameButtonScript : MonoBehaviour
{
    public List<PlayerSelectTab> playerSelectedScreens;
    public GameObject inventoryScreenPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGameButtonClick()
    {
        SceneManager.UnloadSceneAsync("PlayGameScene");
        SceneManager.LoadScene("MauriceScene");

        StartCoroutine(StartGame());
    }
    public IEnumerator StartGame()
    {
        
        string sceneName = "MauriceScene";
        Scene loadedScene = SceneManager.GetSceneByName(sceneName);

        if (!loadedScene.isLoaded)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            loadedScene = SceneManager.GetSceneByName(sceneName);
        }

        if (loadedScene.isLoaded)
        {
            BoardController boardController = loadedScene.GetRootGameObjects().FirstOrDefault(go => go.name == "BoardController")?.GetComponent<BoardController>();

            float yChord = -164f;
            foreach (PlayerSelectTab playerSelectTab in playerSelectedScreens)
            {
                GameObject player = new GameObject("Player" + playerSelectTab.PlayerNameText);
                player.transform.SetParent(boardController.transform); // Attach to the root object of the scene
                PlayerController playerController = player.AddComponent<PlayerController>();
                playerController.playerInventory = new PlayerInventory();
                GameObject inventoryScreen = Instantiate(inventoryScreenPrefab, player.transform);

                playerController.Initialize(playerSelectTab.PlayerImage.sprite, yChord++); // Pass player-specific parameters
                boardController.players.Add(playerController);
            }
        }
       
    }

    public void BackToMainMenu()
    {
        SceneManager.UnloadSceneAsync("PlayGameScene");
        SceneManager.LoadScene("MainMenuScene");
    }
}
