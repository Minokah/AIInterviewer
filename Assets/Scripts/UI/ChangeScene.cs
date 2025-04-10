using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    private Button button;

    public string scene;
    // this class adds a function to switch between the VR and Non-VR mode, upon clicking a main menu button. This class will attach the functionality defined here to a button
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
    }

    private void Clicked()
    {
        SceneManager.LoadScene(scene);
    }
}
