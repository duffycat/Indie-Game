using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text endText;

    // Start is called before the first frame update
    void Start()
    {
        SetDialogueText(null, false);
    }

    public void SetDialogueText(string message, bool toggle)
    {
        if (toggle == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            endText.text = message;
        }
        dialoguePanel.SetActive(toggle);
    }
}
