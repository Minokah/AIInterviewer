using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartInterview : MonoBehaviour
{
    Button Button;
    public InterviewHandler InterviewHandler;
	// This class calls the InterviewHandler to reset the interview and start it from scratch. 
	// It also attaches that function to a button on the main menu UI, that the user will select when starting a new practice interview
	// This ensures proper functionality of the interview
    void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(Begin);
    }

    private void Begin()
    {
        GetComponent<UISwitchPanelsWithPan>().Switch();
        InterviewHandler.RestartInterview();
    }
}
