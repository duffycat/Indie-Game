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
        SceneManager.LoadScene(sceneIndex);
    }

    public DialogueUI ui;
    [TextArea]
    public string dialogue;

    public void Info()
    {
        ui.SetDialogueText(dialogue, true);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }
}
