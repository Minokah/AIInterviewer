using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VROpenKeyboard : MonoBehaviour
{
    private TMP_InputField input;
    public NonNativeKeyboard keyboard;

    void Start()
    {
        input = GetComponent<TMP_InputField>();
        input.onSelect.AddListener(ShowKeyboard);
    }

    // This class is attached to a UI text input element. When the element is selected, this class calls the 3rd party VR keyboard to appear and allow user input, directed into the input text element
    public void ShowKeyboard(string s)
    {
        keyboard.PresentKeyboard(input.text);
    }
}
