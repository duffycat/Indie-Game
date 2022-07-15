using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cheese : Interactable
{
    public delegate void GotCheeseDel();
    public event GotCheeseDel GotCheeseEvent = delegate { };
    public delegate void GoalDel(string text);
    public event GoalDel GoalEvent = delegate { };

    public override void Activate()
    {
        Debug.Log("cheese");
        gameObject.SetActive(false);
        
        GotCheeseEvent.Invoke();
        GoalEvent.Invoke("RUN TO DOOR");
    }
}
