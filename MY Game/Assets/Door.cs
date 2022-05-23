using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    public int sceneIndex = 0;
    bool gotCheese = false;

    public override void Activate()
    {
        if(gotCheese == true) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(sceneIndex);
            Debug.Log("work");
        }
        else 
        {
            Debug.Log("not work");
        }
    }

    public void UnlookDoor() 
    {
        gotCheese = true;
    }

    private void Start()
    {
        GameManger.Instance.Cheese.GotCheeseEvent += UnlookDoor;
    }
}
