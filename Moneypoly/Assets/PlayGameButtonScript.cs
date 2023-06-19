using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameButtonScript : MonoBehaviour
{
    public List<PlayerSelectTab> playerSelectedScreens;
    public PlayerSelectionManager playerSelectionManager;
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
        StartCoroutine(LoadSceneAndSetPlayerSelections());
    }

    private IEnumerator LoadSceneAndSetPlayerSelections()
    {
        // Load the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MauriceScene", LoadSceneMode.Additive);

        // Wait until the scene is fully loaded
        while (!asyncLoad.isDone)
        {
            yield return null;
            // Once the scene is loaded, set player selections
        }

        // Once the scene is loaded, set player selections
        playerSelectionManager.SetPlayerSelections(playerSelectedScreens);
    }
    public void BackToMainMenu()
    {
        SceneManager.UnloadSceneAsync("PlayGameScene");
        SceneManager.LoadScene("MainMenuScene");
    }
}
