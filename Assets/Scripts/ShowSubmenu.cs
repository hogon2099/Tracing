using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSubmenu: MonoBehaviour {

    Canvas thisCanvas;
    public void Hide()
    {
        thisCanvas = GetComponent<Canvas>();
        thisCanvas.enabled = false;
    }
    public void Show()
    {
        thisCanvas = GetComponent<Canvas>();
        thisCanvas.enabled = true;
    }


}
