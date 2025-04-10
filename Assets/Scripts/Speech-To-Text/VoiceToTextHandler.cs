using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Whisper;
using Whisper.Utils;
// This class implements the 3rd party speech to text technolgy and attaches it to our UI interface
public class VoiceToTextHandler : MonoBehaviour
{
    public CanvasGroup SelectionPanel, StopPanel;
    public Button BeginButton, StopButton, CancelButton, CorrectionsButton;
    public TMP_Text ResponseText;
    public TMP_InputField CorrectionsInput;

    public WhisperManager whisper;
    public MicrophoneRecord microphoneRecord;

    private string buffer = "";

    void Start()
    {
		// Using the defined UI objects, we attach functionality to them
        BeginButton.onClick.AddListener(StartRecording);
        StopButton.onClick.AddListener(StopRecording);
        CancelButton.onClick.AddListener(CancelRecording);
        CorrectionsButton.onClick.AddListener(OpenCorrections);
        microphoneRecord.OnRecordStop += DoneRecording;
        whisper.OnNewSegment += AppendText;
    }
	// When user hits the record button we update the UI to inform the user, and start listening for voice input.
    private void StartRecording() {
        SelectionPanel.alpha = 0.1f;
        SelectionPanel.interactable = false;
        StopPanel.alpha = 1;
        StopPanel.interactable = true;
        ResponseText.text = "Listening...";
        microphoneRecord.StartRecord();
    }
	// When user hits the stop button we update the UI to inform the user, and stop listening for voice input
    private void StopRecording() {
        StopPanel.alpha = 0;
        StopPanel.interactable = false;
        microphoneRecord.StopRecord();
    }
	// This method handles cases where the recording is cancled by the user. That's they want to stop and not save any of the speech as text
    private void CancelRecording() {
        microphoneRecord.StopRecord();
        buffer = null;
    }

    // When mic stops, we process the speech into text
    private async void DoneRecording(AudioChunk recordedAudio) {
        buffer = "";
        var res = await whisper.GetTextAsync(recordedAudio.Data, recordedAudio.Frequency, recordedAudio.Channels);
        if (res == null || !ResponseText || res.Result.Equals("[BLANK_AUDIO]")) {
            CancelButton.GetComponent<UISwitchPanels>().Switch();
            return;
        }
    }

    // After processing, display text
    private void AppendText(WhisperSegment segment) {
        buffer += segment.Text;
        buffer = buffer.Replace("[BLANK_AUDIO]", "");
        ResponseText.text = buffer;
        SelectionPanel.alpha = 1;
        SelectionPanel.interactable = true;
    }
	// This is just a debug method to see what exactly the microphone and speech to text system have been picking up
    private void OpenCorrections() {
        CorrectionsInput.text = buffer;
    }
}
