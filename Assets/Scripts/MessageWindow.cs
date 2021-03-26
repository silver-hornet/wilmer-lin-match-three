using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectXformMover))] // To ensure that the message window game object always has the RectXformMover attached to it
public class MessageWindow : MonoBehaviour
{
    public Image messageIcon;
    public Text messageText;
    public Text buttonText;

    public void ShowMessage(Sprite sprite = null, string message = "", string buttonMsg = "start")
    {
        if (messageIcon != null)
        {
            messageIcon.sprite = sprite;
        }

        if (messageText != null)
        {
            messageText.text = message;
        }

        if (buttonText != null)
        {
            buttonText.text = buttonMsg;
        }
    }
}
