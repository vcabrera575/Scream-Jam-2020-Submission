using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public bool flicker = false;
    public new Light light;
    public Light halo;
    

    // Update is called once per frame
    void Update()
    {
        if (light == null)
            return;

        StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        float flickerSpeed = 5f;
        int randomizer = 0;
        while (true)
        {
            if (randomizer >= 99)
            {
                light.enabled = false;
            }
            else
                light.enabled = true;
            randomizer = Random.Range(0, 100);
            yield return new WaitForSeconds(flickerSpeed);
        }
    }
}
