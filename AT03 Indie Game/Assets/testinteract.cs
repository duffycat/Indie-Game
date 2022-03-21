using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testinteract : MonoBehaviour, IInteractable
{
    private bool exampleBool;

    public bool ExampleBool 
    {
        get { return exampleBool; }
    }

    public delegate void InteractableDelegate();

    public event InteractableDelegate interactableDelegate = delegate { };

    private void OnEnable()
    {
        interactableDelegate += TestMethod;
        interactableDelegate += TestMethodTwo;
    }

    private void OnDisable()
    {
        interactableDelegate -= TestMethod;
        interactableDelegate -= TestMethodTwo;
    }


    public void Activate() 
    {
        interactableDelegate.Invoke();
    }

    private void TestMethod() 
    {
        Debug.Log("1 has been executed");
    }

    private void TestMethodTwo() 
    {
        Debug.Log("2 has been executed");
    }
}
