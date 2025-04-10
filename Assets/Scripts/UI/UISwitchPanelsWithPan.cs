using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitchPanelsWithPan : UISwitchPanels
{
    public GameObject GoalCamera, FromCamera;

    // This is a subclass is attached to particular UI objects in Unity. 
	// As acording to its parent functionality, it will allow that button to enable or disable the ability to see/use a UI element, upon user input.
	// However it will also simultaneously adjust the camera angle of the application, so that the camera is focused on the right UI elements
    public override void Start()
    {
        base.Start();
    }

    public override void Switch()
    {
        if (GoalCamera != null && FromCamera != null)
        {
            FromCamera.SetActive(false);
            GoalCamera.SetActive(true);
        }
        
        base.Switch();
    }
}
