using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class InterviewHandler : MonoBehaviour
{
	// link up UI elements and other game objecs the user might interact with during the interview
    public Button RestartButton, KeyboardConfirmButton, VoiceConfirmButton;
    public TMP_Text SpeakerText, UserNameText, UserText, ErrorText, VoiceUserText;
    public TMP_InputField KeyboardInputField;
    public CanvasGroup UserCanvas;
    public GameObject TextPanel, KeyboardPanel, VoicePanel, EndMessagePanel;
    public Dropdown AIDropDownMenu;
    public AIChatController OllamaController;
    public AIChatController GPTController;
    public AIChatController AIChatController;
	public SpeechOnClick SpeechOnClick;
    public UITranscriptHandler transcriptPage;
	public UISpeakerTextHandler speachTextHandler, userTextHandler;
    UIPanelActive UserCanvasScript;

    string state = "wait";

    string text = "Hello [PlayerName]! I will be your AI Interviewer for today. I see that you're interviewing for a [JobPosition] position! I will do my best to recreate this scenario as if it was a real interview. Let's begin!";
    int position = 0;
    float time = 0;

    void Start()
    {
        UserCanvasScript = UserCanvas.gameObject.GetComponent<UIPanelActive>();
        RestartButton.onClick.AddListener(RestartInterview);
        KeyboardConfirmButton.onClick.AddListener(SendFromKeyboard);
        VoiceConfirmButton.onClick.AddListener(SendFromVoice);
    }
	// Resets the practice interview to a default state, so the AI forgets all details from the previous interview and will properly setup the interview
	// Also resets any UI elements that may have been changed during the last interview, like the transcript page
	// This is called when a new interview is started or if the current interview is restarted
    public void RestartInterview()
    {
        int menuIndex = AIDropDownMenu.value;
        List<Dropdown.OptionData> menuOptions = AIDropDownMenu.options;
        string chosenInstance = menuOptions[menuIndex].text;
        if (chosenInstance == "External Instance")
        {
            AIChatController = GPTController;
            Debug.Log("Using the External Instance");
        }
        else
        {
            AIChatController = OllamaController;
            Debug.Log("Using the Ollama Instance");
        }

        transcriptPage.ResetTranscript();
		speachTextHandler.ResetTextSize();
		userTextHandler.ResetTextSize();

        TextPanel.SetActive(true);
        KeyboardPanel.SetActive(false);
        VoicePanel.SetActive(false);

        state = "respond";

        UserNameText.text = "Player";
        UserText.text = "Click Voice or Keyboard to begin response";
        ErrorText.text = "";

        AIChatController.ClearMessages();
        StartCoroutine(AIChatController.Request("Hi!"));
        time = 0;
        position = 0;
		
		EndMessagePanel.SetActive(false);
    }

    // Checks and updates the UI screen depending on what is happening in the system
    void Update()
    {
        switch (state)
        {
			// If The AI is preparing a response
            case "wait":
                if (time < 0.015f)
                {
                    time += Time.deltaTime;
                    return;
                }
				// Indicate to the user that the interview has finished, if that has happened
				if (SpeakerText.text.Contains("---THE INTERVIEW HAS FINISHED---")){
					state = "end";
					return;
				}

				// Don't let the user enter any input while the AI is still outputting what it has said
				// Else you can start allowing the user to start their input
                if (position < text.Length + 1)
                {
                    UserCanvasScript.enabled = false;
                    UserCanvas.interactable = false;
                    UserCanvas.alpha = 0.1f;
                    SpeakerText.text = text.Substring(0, position);
                    position++;
                    time = 0;
                }
                else
                {
                    UserCanvasScript.enabled = true;
                    UserCanvas.interactable = true;
                    UserCanvas.alpha = 1f;
                }
                break;
			// Once the user has been allowed to respond, they enter this state and can start entering their input
            case "respond":
				// get and display the AI's response as text 
                string responseText = AIChatController.GetRecentResponse();	
				// once the user submits their answer back to the AI, we switch back into a state where the user must wait for the AI to respond
                if (responseText != null && !responseText.Equals(""))
                {
					text = responseText;
					state = "wait";
					time = 0;
					position = 0;
					
					SpeechOnClick.OnNewMessage(text);
					
					return;
                }

                if (time < 0.15f)
                {
                    time += Time.deltaTime;
                    return;
                }

                SpeakerText.text = "......".Substring(0, position);
                position++;
                time = 0;

                if (position == 7) position = 0;
                break;
			// Once the interview has finished we disable the UI that allows user input	
			case "end":
				TextPanel.SetActive(false);
				EndMessagePanel.SetActive(true);
				break;
        }
    }

	// Prepare to send the latest user dialogue in text form to the AI. This method is used if the user utilized manual keyboard style input in their response
    private void SendFromKeyboard() {
        if (KeyboardInputField.text.Equals(""))
        {
            ErrorText.text = "Field blank; Try Again";
            return;
        }
		else{
			ErrorText.text = "";
		}

        GetAIResponse(KeyboardInputField.text);
    }

	// Prepare to send the latest user dialogue in text form to the AI. This method is used if the user utilized speech-to-text feature in their response
    private void SendFromVoice() {
        GetAIResponse(VoiceUserText.text);
    }

	// This method will send the user's response, in the form of text, to the AI Interviewer instance
    private void GetAIResponse(string text)
    {
		// update the transcript page after the user has entered their latest response
        transcriptPage.AddNewUserDialogue(text);

        // Certain symbols cause issues in the webrequest sent to the AI, so we catch and replace them with valid symbols
        text = text.Replace("\"", "\\\"");
        text = text.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ");

		// The following code enables UI elements to display the AI Interviewer's reply in a valid manner
        state = "respond";
        position = 0;
        time = 0;
        UserCanvasScript.enabled = false;
        UserCanvas.interactable = false;
        UserCanvas.alpha = 0.1f;
        UserText.text = text;
		// Start the AI Interviewer's response to the user's response
        StartCoroutine(AIChatController.Request(text));
    }
}
