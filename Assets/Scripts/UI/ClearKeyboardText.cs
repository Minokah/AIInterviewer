using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClearKeyboardText : MonoBehaviour
{
    private Button button;

    public TMP_InputField input;
    
    // This class is used by the VR keyboard to clear the input text that the user is trying to send, if they want to restart and try to input something else
	// The VR keyboard itself and its functionality is a 3rd party resource whose code is elsewhere
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ClearInput);
    }

    private void ClearInput()
    {
        input.text = "";
    }
}
