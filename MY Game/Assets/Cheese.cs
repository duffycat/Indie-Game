using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cheese : Interactable
{
    public delegate void GotCheeseDel();
    public event GotCheeseDel GotCheeseEvent = delegate { };

    public override void Activate()
    {
        Debug.Log("cheese");
        gameObject.SetActive(false);
        
        GotCheeseEvent.Invoke();

    }
}
