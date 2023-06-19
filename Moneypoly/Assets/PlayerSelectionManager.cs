using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectionManager : MonoBehaviour
{
    public GameObject inventoryScreenPrefab;
    private GameObject playerPrefab; // Reference to the player prefab in the Inspector

    public string playerPrefabPathUnity = "Assets/Prefabs/Player.prefab";
    public string playerPrefabPath = "Prefabs/Player";

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerPrefab = null;

        #if UNITY_EDITOR
            playerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(playerPrefabPathUnity);

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
        float yChord = -164f;
        Scene loadedScene = SceneManager.GetSceneByName("MauriceScene");
        BoardController boardController = loadedScene.GetRootGameObjects().FirstOrDefault(go => go.name == "BoardController")?.GetComponent<BoardController>();
        foreach (PlayerSelectTab playerSelectTab in playerSelectedScreens)
        {
            try
            {
                GameObject player = Instantiate(playerPrefab); // Instantiate the player prefab
                player.name = "Player" + playerSelectTab.PlayerNameText;
                player.transform.SetParent(boardController.transform); // Attach to the root object of the scene

                PlayerController playerController = player.GetComponent<PlayerController>(); // Get the PlayerController component from the prefab
                playerController.playerInventory = new PlayerInventory();

                GameObject inventoryScreen = Instantiate(inventoryScreenPrefab, player.transform);

                playerController.Initialize(playerSelectTab.PlayerImage.sprite, yChord++); // Pass player-specific parameters
                boardController.players.Add(playerController);
            }
            catch (System.Exception)
            {
                Debug.Log("problem");
                SetPlayerSelections(playerSelectedScreens); 
                throw;
            }
           
        }

        SceneManager.UnloadSceneAsync("PlayGameScene");
    }
}
