using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDemo : MonoBehaviour
{
    private void Start()
    {
        ExamplePopup.OnOpen += PopupOpened;
        ExamplePopup.OnClosed += PopupClosed;
    }

    private void PopupOpened()
    {
        Debug.Log("Popup is opened");
    }
    
    private void PopupClosed()
    {
        Debug.Log("Popup is Closed");
    }
    
    public void ShowPanel()
    {
        ExamplePanel.Open();
    }

    public void ShowPopup()
    {
        ExamplePopup.Open();
    }
}
