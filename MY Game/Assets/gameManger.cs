using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [SerializeField] private Cheese cheese;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Door door;

    public Cheese Cheese { get { return cheese; } }
    public Enemy Enemy { get { return enemy; } }
    public Door Door { get { return door; } }

    public static GameManger Instance { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Cheese.gameObject.activeSelf == false) 
        {
            
        }
    }
}
