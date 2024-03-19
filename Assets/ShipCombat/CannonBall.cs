using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public float damage = 50.0f;
    public GameObject impact;
    public AudioSource cannonAudio;
    public AudioClip impactSFX;

    private MeshRenderer meshRenderer;
    private BoxCollider boxCollider;

    void Start()
    {
        Destroy(this.gameObject, 5.0f);
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<EnemyShip>().TakeDamage(damage);
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        Destroy(this.gameObject, 3.0f);
        cannonAudio.PlayOneShot(impactSFX);
        GameObject effect = Instantiate(impact, transform.position, transform.rotation);
        Destroy(effect, 2.5f);
    }
}
