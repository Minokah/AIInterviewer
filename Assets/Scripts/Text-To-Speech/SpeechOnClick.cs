using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Piper;

public class SpeechOnClick : MonoBehaviour
{
    public PiperManager piper;
    public TMP_Text robotText;
    public Button submitButton;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        submitButton.onClick.AddListener(OnButtonPressed);
    }

    private void OnButtonPressed()
    {
        var text = robotText.text;
        OnInputSubmit(text);
    }

    private async void OnInputSubmit(string text)
    {
        var audio = piper.TextToSpeech(text);

        source.Stop();
        if (source && source.clip)
        {
            Destroy(source.clip);
        }

        source.clip = await audio;
        source.Play();
    }
	
	public async void OnNewMessage(string text){
		var audio = piper.TextToSpeech(text);

        source.Stop();
        if (source && source.clip)
        {
            Destroy(source.clip);
        }

        source.clip = await audio;
        source.Play();
	}
	
	public async void PauseExisting(){
        source.Stop();
        if (source && source.clip)
        {
            Destroy(source.clip);
        }
	}
}