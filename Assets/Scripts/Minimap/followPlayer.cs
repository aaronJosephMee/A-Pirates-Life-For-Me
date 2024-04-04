using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offsetX, offsetZ;
    [SerializeField] private float LerpSpeed;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
            new Vector3(target.position.x + offsetX, transform.position.y, target.position.z + offsetZ), LerpSpeed);
        transform.rotation = Quaternion.Euler(90f, target.eulerAngles.y, 0f);

    }

}
