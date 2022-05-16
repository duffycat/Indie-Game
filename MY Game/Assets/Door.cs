using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    public int sceneIndex = 0;
    bool gotCheese = true;

    public override void Activate()
    {
        if(gotCheese == true) 
        {
            SceneManager.LoadScene(sceneIndex);
            Debug.Log("work");
        }
        else 
        {
            Debug.Log("not work");
        }
    }
}
