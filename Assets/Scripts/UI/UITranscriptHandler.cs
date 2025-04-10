using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
// This class handles the formatting, populating, and restting of the UI transcript menu
public class UITranscriptHandler : MonoBehaviour
{
    public GameObject interviewerDialogueTemplate;
    public GameObject userDialogueTemplate;
    public Transform contentBox;

    public List<GameObject> textBoxes = new List<GameObject>();
    int interviewTextCount = 0;
    int userTextCount = 0;
    
    float originalWindowHeight;
    public RectTransform rt;
    Vector2 sizeDelta;

    void Start()
    {
        sizeDelta = rt.sizeDelta;
        originalWindowHeight = sizeDelta.y;
    }
	// Adds a new AI dialogue box chronologically after the last AI dialogue, and below it
	// It will work by creating and making visible a dialogue box object, and then just updating its vertical position and text content, to reflect what was actually said by the AI
	// adjusting vertical space also ensures that the vertical scrollbar feature works without issue, allowing user to fully view all dialogue boxes
    public void AddNewInterviewerDialogue(string text)
    {
		GameObject newInterviewer = Instantiate(interviewerDialogueTemplate, interviewerDialogueTemplate.transform.position, interviewerDialogueTemplate.transform.rotation);
		newInterviewer.transform.SetParent(contentBox);
        newInterviewer.transform.localScale = new Vector3(1, 1, 1);
		newInterviewer.name = "AI Text Box: " + interviewTextCount;

		// beyond the first dialogue box, we will need to adjust its vertical position by factoring the size of the last text box, and also the length of the last dialogue text
		// this makes sure that dialogue boxes do not overlap each other
        if (interviewTextCount > 0)
        {
			float lastTextPosition = textBoxes.Last().transform.localPosition.y;
			float mostRecentTextHeight = textBoxes.Last().transform.GetChild(1).gameObject.GetComponent<TMP_Text>().preferredHeight;
			if (mostRecentTextHeight > 190) {
				newInterviewer.transform.localPosition =  new Vector3(newInterviewer.transform.localPosition.x, lastTextPosition - mostRecentTextHeight - 40, newInterviewer.transform.localPosition.z);
				sizeDelta.y = sizeDelta.y + mostRecentTextHeight + 140;
			}	
			else {
				newInterviewer.transform.localPosition =  new Vector3(newInterviewer.transform.localPosition.x, lastTextPosition - 230, newInterviewer.transform.localPosition.z);
				sizeDelta.y = sizeDelta.y + 330;
			}
            rt.sizeDelta = sizeDelta;
        }

		// update textbox contents
        GameObject textObject = newInterviewer.transform.GetChild(1).gameObject;
        TMP_Text textArea = textObject.GetComponent<TMP_Text>();
        textArea.text = text;
		// update number of dialogue boxes, to be used when adjusting future vertical positions of dialogue boxes
        textBoxes.Add(newInterviewer);
        interviewTextCount = interviewTextCount + 1;

        newInterviewer.SetActive(true);
    }
	// Adds a new user dialogue box chronologically after the last user dialogue, and below it
	// Except for some different UI positioning values, it works the exact same as the above method
    public void AddNewUserDialogue(string text)
    {
		GameObject newUser = Instantiate(userDialogueTemplate, userDialogueTemplate.transform.position, userDialogueTemplate.transform.rotation);
        newUser.transform.SetParent(contentBox);
        newUser.transform.localScale = new Vector3(1, 1, 1);
		newUser.name = "User Text Box: " + userTextCount;
				
		// We will need to adjust dialogue box vertical position by factoring the size of the last text box, and also the length of the last dialogue text
		// this makes sure that dialogue boxes do not overlap each other
		float lastTextPosition = textBoxes.Last().transform.localPosition.y;			
		float mostRecentTextHeight = textBoxes.Last().transform.GetChild(1).gameObject.GetComponent<TMP_Text>().preferredHeight;	
		if (mostRecentTextHeight > 190) {
			newUser.transform.localPosition =  new Vector3(newUser.transform.localPosition.x, lastTextPosition - mostRecentTextHeight - 40, newUser.transform.localPosition.z);
			sizeDelta.y = sizeDelta.y + mostRecentTextHeight + 140;
		}	
		else {
			newUser.transform.localPosition =  new Vector3(newUser.transform.localPosition.x, lastTextPosition - 230, newUser.transform.localPosition.z);
			sizeDelta.y = sizeDelta.y + 330;
		}		
		
		rt.sizeDelta = sizeDelta;
		// update textbox contents
        GameObject textObject = newUser.transform.GetChild(1).gameObject;
        TMP_Text textArea = textObject.GetComponent<TMP_Text>();
        textArea.text = text;
		// update number of dialogue boxes, to be used when adjusting future vertical positions of dialogue boxes
        textBoxes.Add(newUser);
        userTextCount = userTextCount + 1;

        newUser.SetActive(true);
    }
	// After an interview is restarted or a new one begins, we clear all previous transcript text and reset the transcript UI size
	// Also clear the list of dialogue boxes being tracked
    public void ResetTranscript()
    {
		GameObject textBoxToRemove;
        for (int i=textBoxes.Count - 1; i>=0; i--)
        {
			textBoxToRemove = textBoxes[i];
			textBoxes.RemoveAt(i);
            Destroy(textBoxToRemove);
        }
        sizeDelta.y = originalWindowHeight;
        rt.sizeDelta = sizeDelta;

        interviewTextCount = 0;
        userTextCount = 0;
    }
}
