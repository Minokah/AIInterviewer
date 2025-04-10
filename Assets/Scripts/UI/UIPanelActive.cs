using UnityEngine;
// This class is used to make a entire UI canvas panal visible or invisible to the user
// The script is attached manually to UI objects
public class UIPanelActive : MonoBehaviour
{
    CanvasGroup canvas;
    void Awake()
    {
        canvas = GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (canvas.alpha < 1) canvas.alpha += 5 * Time.deltaTime;
    }

    public void ZeroAlpha()
    {
        canvas.alpha = 0;
    }
}
