using UnityEngine;

namespace DefaultNamespace.MapScreen
{
    public class MapInteractable : MonoBehaviour, IInteractable
    {
        private PlayerController _playerController;
        private GameObject instance;
        public bool spawned = false;
        public GameObject toSpawn;
        
        void Update()
        {
            if (spawned && instance == null){
                spawned = false;
                _playerController.EnablePlayerInput();
            }
        }
        public void Interact(Collider collider)
        {
            if (!spawned && !GameManager.instance.menuOpen)
            {
                _playerController = collider.gameObject.GetComponent<PlayerController>();
                if (_playerController != null)
                {
                    _playerController.DisablePlayerInput();
                }
                spawned = true;
                instance = Instantiate(toSpawn);
            }

        }
    }
}