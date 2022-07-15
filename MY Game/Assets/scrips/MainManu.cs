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
    private int currentButtonIndex = 2;

    private void Awake()
    {
        
    }

    private void Update()
    {
        switch (currentButtonIndex)
        {
            case -1:
                currentButtonIndex = 2;
                break;
            case 2:
                playBtn.Select();
                //Debug.Log(currentButtonIndex);
                break;
            case 1:
                infoBtn.Select();
                //Debug.Log(currentButtonIndex);
                break;
            case 0:
                quitBtn.Select();
                //Debug.Log(currentButtonIndex);
                break;
            case 3:
                currentButtonIndex = 0;
                break;
        }

        if (timer < 0)
        {
            float move = Input.GetAxisRaw("Vertical");
            if (move > 0)
            {
                currentButtonIndex++;
            }
            else if (move < 0)
            {
                currentButtonIndex--;
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
        if (currentButtonIndex == 2)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        currentButtonIndex = 2;
    }

    public DialogueUI ui;
    [TextArea]
    public string dialogue;

    public void Info()
    {
        if (currentButtonIndex == 1)
        {
            ui.SetDialogueText(dialogue, true);
        }
        currentButtonIndex = 1;
    }

    public void QuitGame() 
    {
        if (currentButtonIndex == 0)
        {
            Application.Quit();
        }
        currentButtonIndex = 0;
    }
}
