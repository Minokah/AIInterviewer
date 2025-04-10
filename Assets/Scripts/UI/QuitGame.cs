using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class used by buttons to enable user to quit game on click
public class QuitGame : MonoBehaviour {
	Button Button;
	void Start()
    {
        Button = GetComponent<Button>();
        Button.onClick.AddListener(Exit);
    }

    public void Exit()
    {
		// When quiting in editor we just want to stop the editor from playing the game
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		// Otherwise we will exit the game for real	
		#else
			Application.Quit();
		#endif
    }
}
