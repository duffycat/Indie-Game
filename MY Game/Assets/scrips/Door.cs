using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    public int sceneIndex = 0;
    bool gotCheese = false;
    public delegate void VictoryDel(string text);
    public event VictoryDel VictoryEvent = delegate { };

    public override void Activate()
    {
        if(gotCheese == true) 
        {
            GameManger.Instance.Enemy.Agent.speed = 0f;
            VictoryEvent.Invoke("You Win");
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
