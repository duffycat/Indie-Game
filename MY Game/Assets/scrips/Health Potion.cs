using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Interactable
{
    public override void Activate()
    {
        
        gameObject.SetActive(false);
    }
}
