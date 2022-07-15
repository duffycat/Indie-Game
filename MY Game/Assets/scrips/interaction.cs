using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interaction : MonoBehaviour
{
    public float distance;

    void Update()
    {
        if(Input.GetButtonDown("Fire1") == true) 
        {
            Debug.DrawRay(transform.position, transform.forward * distance, Color.blue, 0.2f);
            RaycastHit hit;
            if(Physics.Raycast(transform.position, transform.forward, out hit, distance) == true) 
            {
                Debug.DrawRay(transform.position, transform.forward * distance, Color.red, 0.2f);
                if(hit.collider.TryGetComponent(out IInteractable interaction) == true) 
                {
                    interaction.Activate();
                }
            }
        }
    }
}

public interface IInteractable
{
    public abstract void Activate();

}

public abstract class Interactable : MonoBehaviour,IInteractable
{
    public virtual void Activate()
    { }
}

