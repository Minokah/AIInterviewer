using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
// This class is used to constantly update the UI size of a text window it is attached to in the interview interface UI
// By doing so, it allows the UI scrollbar to appear at appropriate times for when the text would overflow the UI windows.
// By adjusting UI size it also adjusts the size and the scroll capability of the scroll bar, so the scroll bar looks and functions appropriately for scrolling text
// Lastly, it will reset the size of the UI text window when the interview is restarted or when a new one starts, ensuring proper functionality
public class UISpeakerTextHandler : MonoBehaviour
{
    float originalWindowHeight;
    public RectTransform rt;
	public TMP_Text speakerText;
    Vector2 sizeDelta;

    void Start()
    {
        sizeDelta = rt.sizeDelta;
        originalWindowHeight = sizeDelta.y;
    }

    void Update()
    {
		float mostRecentTextHeight = speakerText.preferredHeight;
		sizeDelta.y = mostRecentTextHeight + 20;
        rt.sizeDelta = sizeDelta;
    }

    public void ResetTextSize()
    {
        sizeDelta.y = originalWindowHeight;
        rt.sizeDelta = sizeDelta;
    }
}
