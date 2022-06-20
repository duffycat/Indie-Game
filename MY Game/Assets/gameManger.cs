using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    [SerializeField] private Cheese cheese;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Door door;
    [SerializeField] private Canvas canvas;

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
        Instance.door.VictoryEvent += GameOver;
        Instance.enemy.EndEvent += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if(Cheese.gameObject.activeSelf == false) 
        {
            
        }

        if(Input.GetButtonDown("Cancel") == true) 
        {
            Debug.Log("Qu");
            Application.Quit();
        }
    }

    public EndUI ui;
    [SerializeField] private Button EndBtn;

    public void GameOver(string text)
    {
        ui.SetDialogueText(text, true);
    }

    public void End() 
    {
        SceneManager.LoadScene(0);
    }
}
