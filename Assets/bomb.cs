using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public ParticleSystem bombEffect;
    public GameObject explosionSphere;

    public float explosionDuration = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ActivateAfterDelay(7f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void explode()
    {
        bombEffect.Play();
        MeshRenderer mesh = GetComponent<MeshRenderer>();
        if (mesh != null)
        {
            mesh.enabled = false;
        }
        MeshCollider meshC = GetComponent<MeshCollider>();
        if (meshC != null)
        {
            meshC.enabled = false;
        }



        explosionSphere.SetActive(true);

        StartCoroutine(DeactivateAfterDelay(explosionSphere, explosionDuration));

        CapsuleCollider capC = GetComponent<CapsuleCollider>();
        if (capC != null)
        {
            capC.enabled = false;
        }

        StartCoroutine(DestroyAfterDelay(7f));
    }

    IEnumerator ActivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        explode();
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    IEnumerator DeactivateAfterDelay(GameObject gameObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }
}
