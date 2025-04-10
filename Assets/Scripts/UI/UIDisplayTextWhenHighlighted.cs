using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDisplayTextWhenHighlighted : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string HighlightText;
    public TMP_Text TextObject;

	// This class is used to display UI text when the user hovers over a certain UI element that has this script attached to it. It then hides the text when the user no longer is hovering over it
	// The script is attached manually to various UI objects
    public void OnPointerEnter(PointerEventData eventData)
    {
        TextObject.text = HighlightText;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TextObject.text = "";
    }
}
