using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheese : Interactable
{
    public override void Activate()
    {
        Debug.Log("cheese");
        gameObject.SetActive(false);
        //
    }
}
