using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RustyController : MonoBehaviour
{
    // Start is called before the first frame update
    private SkinnedMeshRenderer mesh;
    public Material redMaterial;
    public Material blueMaterial;
    void Start()
    {
        mesh = transform.Find("Character_Skeleton_03").GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRed()
    {
        mesh.material = redMaterial;
    }

    public void SetPurple()
    {
        mesh.material = blueMaterial;
    }
}
