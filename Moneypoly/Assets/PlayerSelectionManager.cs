using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectionManager : MonoBehaviour
{
    public GameObject inventoryScreenPrefab;
    public GameObject playerPrefab; // Reference to the player prefab in the Inspector

    public string playerPrefabPathUnity = "Assets/Prefabs/Player";
    public string playerPrefabPath = "Prefabs/Player";

    // Start is called before the first frame update
    void Start()
    {
        

        #if UNITY_EDITOR
            //playerPrefab = AssetDatabase.FindAssets<GameObject>(playerPrefabPathUnity);

        #else
            playerPrefab = Resources.Load<GameObject>(playerPrefabPath);
            if (playerPrefab == null)
            {
                Debug.LogError("Player prefab not found in Resources!");
            }
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetPlayerSelections(List<PlayerSelectTab> playerSelectedScreens)
    {
        float yChord = -158f;
        float xChord = -162.3f;
        Scene loadedScene = SceneManager.GetSceneByName("MauriceScene");
        BoardController boardController = loadedScene.GetRootGameObjects().FirstOrDefault(go => go.name == "BoardController")?.GetComponent<BoardController>();
        foreach (PlayerSelectTab playerSelectTab in playerSelectedScreens)
        {
            try
            {
                if(playerSelectTab.SelectedPlayer != null)
                {
                    GameObject player = Instantiate(playerPrefab); // Instantiate the player prefab
                    player.name = playerSelectTab.PlayerNameText.text;
                    player.transform.SetParent(boardController.transform); // Attach to the root object of the scene
                    //player.transform.SetPositionAndRotation(boardController.transform.position,Quaternion.identity);
                    PlayerController playerController = player.GetComponent<PlayerController>(); // Get the PlayerController component from the prefab
                    GameObject inventoryScreen = Instantiate(inventoryScreenPrefab, player.transform);

                    playerController.Initialize(playerSelectTab.PlayerImage.sprite, yChord, xChord); // Pass player-specific parameters
                    boardController.players.Add(playerController);
                    xChord -= 7f;
                    
                }

            }
            catch (System.Exception e)
            {
                Debug.Log("problem" + e.Message);
                SetPlayerSelections(playerSelectedScreens); 
                throw;
            }
           
        }

        boardController.StartGame();
        SceneManager.UnloadSceneAsync("PlayGameScene");
    }
}
