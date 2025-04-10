using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Piper;

// This class simply adds the pause function to the appropriate UI button in the system interface
// It otherwise just uses the methods from the SpeechOnClick class
public class PauseSpeechOnClick : MonoBehaviour
{
	Button Button;
	public SpeechOnClick SpeechOnClick;
	void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(PauseSpeech);
    }

    public void PauseSpeech()
    {
		SpeechOnClick.PauseExisting();
    }
}