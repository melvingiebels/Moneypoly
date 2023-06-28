using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleVisibilityAndDelete : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerSelectTab SelectTab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDelete()
    {
        SelectTab.gameObject.SetActive(false);
        SelectTab.RemovePlayer();
    }
}
