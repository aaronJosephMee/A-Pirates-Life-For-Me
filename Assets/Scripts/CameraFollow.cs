using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform player;
    public float heightOffset = 1.65f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + heightOffset, player.position.z);

        transform.rotation = player.rotation;
    }
}
