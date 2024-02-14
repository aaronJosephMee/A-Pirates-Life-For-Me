using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class InteractionPoint : MonoBehaviour
{
    public GameObject interactText;
    private IInteractable _interactable;
    // Start is called before the first frame update
    void Start()
    {
        _interactable = gameObject.transform.parent.GetComponent<IInteractable>();
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other) {
        if (Input.GetKey(KeyCode.E))
        {
            _interactable.Interact(other);
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")){
            interactText.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")){
            interactText.SetActive(false);
        }
    }
}
