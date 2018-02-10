using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public string uiMessage;
    public string button;

    public Text text;
    public InteractionHandler handler;

    private bool playerInside;

    void Start()
    {

    }

    void Update()
    {
        if (playerInside && Input.GetButtonDown(button))
        {
            handler.handleAction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerInside = true;
        text.text = uiMessage;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerInside = false;
        text.text = "";
    }
}
