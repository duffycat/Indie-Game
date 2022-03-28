using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testineracttoo : MonoBehaviour, IInteractable
{
    public testinteract interaction;

    private void Start()
    {
        interaction.interactableDelegate += TestMethodThree;
        //interaction.interactableDelegate.Invoke();
        
    }

    public void Activate() 
    {
        Debug.Log("ineract is on" + interaction.ExampleBool);
        interaction.Activate();
    }
    
    private void TestMethodThree() 
    {
        Debug.Log("M3");
    }
}
