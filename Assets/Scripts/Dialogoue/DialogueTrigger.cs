using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<DialogueString> dialogueStrings = new List<DialogueString>();

    private bool hasSpoken = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasSpoken)
        {
            other.gameObject.GetComponent<DialogueManager>().DialogueStart(dialogueStrings);
            hasSpoken = false;
        }
    }
}

[System.Serializable]
public class DialogueString
{
    public string text;
    public bool isEnd;

    [Header("Branch")]
    public bool isQuestion;
    public string answer1;
    public string answer2;
    public int answer1Index;
    public int answer2Index;

    [Header("TriggeredEvents")]
    public UnityEvent startDialogue;
    public UnityEvent endDialogue;
}