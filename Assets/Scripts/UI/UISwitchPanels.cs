using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISwitchPanels : MonoBehaviour
{
    Button Button;
    public GameObject GoalCanvas, FromCanvas;

    // This class is attached to particular UI objects in Unity. It will allow that button to enable or disable the ability to see/use a UI element, upon user input.
    public virtual void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(Switch);
    }

    public virtual void Switch()
    {
        GoalCanvas.SetActive(true);
        GoalCanvas.GetComponent<UIPanelActive>().ZeroAlpha();
        FromCanvas.SetActive(false);
    }
}
