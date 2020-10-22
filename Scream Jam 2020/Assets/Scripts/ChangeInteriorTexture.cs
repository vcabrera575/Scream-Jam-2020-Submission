using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeInteriorTexture : MonoBehaviour
{
    Renderer rend;

    public Texture[] textures;

    // Start is called before the first frame update
    void Start()
    {
        rend = this.GetComponent<Renderer>();
        int rand = Random.Range(0, textures.Length);

        rend.material.mainTexture = textures[rand];
    }

}
