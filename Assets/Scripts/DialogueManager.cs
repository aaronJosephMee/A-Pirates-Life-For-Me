using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Button answer1Button;
    [SerializeField] private Button answer2Button;

    [SerializeField] private float typingSpeed = 0.02f;
    [SerializeField] private float turnSpeed = 1f;

    private List<DialogueString> dialogueList;

    [Header("Player")]
    private Transform playerCamera;
    private int currentDialogueIndex = 0;

    private void Start()
    {
        dialogueParent.SetActive(false);
        playerCamera = Camera.main.transform;
    }
    
    public void DialogueStart(List<DialogueString> TextToPrint, Transform NPCTransform)
    {
        dialogueParent.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        playerMovement.enabled = false;

        StartCoroutine(TurnTowards(NPCTransform));
        dialogueList = TextToPrint;
        currentDialogueIndex = 0;
        DisableButtons();
        StartCoroutine(TypeDialogue());
    }

    private void DisableButtons()
    {
        answer1Button.interactable = false;
        answer2Button.interactable = false;

        answer1Button.GetComponentInChildren<TextMeshProUGUI>().text = "Click to continue";
        answer2Button.GetComponentInChildren<TextMeshProUGUI>().text = "Click to continue";
    }

    private IEnumerator TurnTowards(Transform target)
    {
        Quaternion startRotation = playerCamera.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - playerCamera.position);

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            playerCamera.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * turnSpeed;
            yield return null;
        }
        playerCamera.rotation = targetRotation;
    }

    private bool optionSeleceted = false;

    private IEnumerator TypeDialogue()
    {
       while(currentDialogueIndex < dialogueList.Count)
       {
           DialogueString line = dialogueList[currentDialogueIndex];
           line.startDialogue?.Invoke();

           if(line.isQuestion)
           {
               yield return StartCoroutine(TypeText(line.text));

               answer1Button.interactable = true;
               answer2Button.interactable = true;

               answer1Button.GetComponentInChildren<TextMeshProUGUI>().text = line.answer1;
               answer2Button.GetComponentInChildren<TextMeshProUGUI>().text = line.answer2;

               answer1Button.onClick.AddListener(() => HandleOptionSelected(line.answer1Index));
               answer2Button.onClick.AddListener(() => HandleOptionSelected(line.answer2Index));

               yield return new WaitUntil(() => optionSeleceted);
           }
           else
           {
               yield return StartCoroutine(TypeText(line.text));   
           }
           line.endDialogue?.Invoke();
           optionSeleceted = false;
       }
       DialogueStop();
    }
     
    private void HandleOptionSelected(int index)
    {
        optionSeleceted = true;
        DisableButtons();
        currentDialogueIndex = index;
    }

    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        if(!dialogueList[currentDialogueIndex].isQuestion)
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        if(dialogueList[currentDialogueIndex].isEnd)
        {
            DialogueStop();
        }
        currentDialogueIndex++;
    }

    private void DialogueStop()
    {
        StopAllCoroutines();
        dialogueText.text = "";

        dialogueParent.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        playerMovement.enabled = true;
    }
}

