using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainManu : MonoBehaviour
{
    [SerializeField] public int sceneIndex = 0;

    [SerializeField] private float bufferTime = 1f;

    [SerializeField] private Button playBtn;
    [SerializeField] private Button infoBtn;
    [SerializeField] private Button quitBtn;

    private float timer = -1;
    private int currentButtonIndex = 0;
    private MainManu currentButton;

    private void Awake()
    {
        
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1") == true) 
        {
            switch (currentButtonIndex)
            {
                case 0:
                    LoadNewGame();
                    break;
                case 1:
                    Info();
                    break;
                case 2:
                    QuitGame();
                    break;
            }
        }

        switch (currentButtonIndex)
        {
            case 0:
                
                break;
            case 1:

                break;
            case 2:

                break;
        }

        if (timer < 0)
        {
            float move = Input.GetAxisRaw("MVertical");
            if (move > 0)
            {
                currentButtonIndex++;
                Debug.Log("up");
            }
            else if (move < 0)
            {
                currentButtonIndex--;
                Debug.Log("not up");
            }
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= bufferTime)
            {
                timer = -1;
            }
        }

    }

    public void LoadNewGame() 
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Info()
    {
        
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
