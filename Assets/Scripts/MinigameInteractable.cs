using UnityEngine;

namespace DefaultNamespace
{
    public class MinigameInteractable : MonoBehaviour, IInteractable
    {
        public string scene;
        public void Interact(Collider collider)
        {
            GameManager.instance.LoadScene(scene, false);
        }
    }
}