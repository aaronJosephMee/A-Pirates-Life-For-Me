using DefaultNamespace;
using UnityEngine;

public class InteractionPoint : MonoBehaviour
{
    public GameObject interactText;
    private IInteractable _interactable;
    private bool _canInteract = true;
    // Start is called before the first frame update
    void Start()
    {
        _interactable = gameObject.transform.parent.GetComponent<IInteractable>();
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other) {
        if (Input.GetKey(KeyCode.E) && _canInteract)
        {
            _interactable.Interact(other);
            interactText.SetActive(false);
            _canInteract = false;
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
            _canInteract = true;
        }
    }
}
