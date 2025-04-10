using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

// Define the classes used for parsing. The fields must correspond to the JSON returned by the backend.
// FOR the External ChatGPT Instance; These fields allow response text from the AI to be properly and efficently stored and extracted
[System.Serializable]
public class Message
{
    public string content;
}

[System.Serializable]
public class Choice
{
    public Message message;
}

[System.Serializable]
public class Response
{
    public List<Choice> choices;
}

// FOR the External Ollama Instance; These fields allow response text from the AI to be properly and efficently stored and extracted
[System.Serializable]
public class LlamaMessage
{
    public string role;
    public string content;
}

[System.Serializable]
public class OllamaResponse
{
    public string model;
    public string created_at;
    public LlamaMessage message;

    public string done_reason;
    public bool done;

    public long total_duration;
    public long load_duration;
    public int prompt_eval_count;
    public long prompt_eval_duration;
    public int eval_count;
    public long eval_duration;
}

public class AIChatController : MonoBehaviour
{

    // Define virtual classes that are implemented in the concerte subclasses
	// This file exists so that at run time, either AI instance can be freely swapped with each other, and the system sees them both as "AIChatController"

    public virtual IEnumerator Request(string input)
    {
        yield return new WaitForSecondsRealtime(0);
    }

    public virtual void ClearMessages()
    {

    }

    public virtual string GetRecentResponse()
    {
        return "placeholder";
    }
}
