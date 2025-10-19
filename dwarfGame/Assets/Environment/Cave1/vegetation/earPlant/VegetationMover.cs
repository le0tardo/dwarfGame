using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class VegetationMover : MonoBehaviour
{
    MeshRenderer rend;
    float d = 0.25f;
    float str = 0f;
    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.material.SetFloat("_str",0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(MoveVegetation());
        }
    }

    IEnumerator MoveVegetation()
    {
        float e = 0f;
        float s = 0f;
        float t = 0.025f;

        while(e < d)
        {
            e += Time.deltaTime;
            str = Mathf.Lerp(s, t, e / d);
            rend.material.SetFloat("_str", str);
            yield return null;
        }

        str = t;

        e=0f;
        while(e < d)
        {
            e+= Time.deltaTime;
            str = Mathf.Lerp(t, s, e / d);
            rend.material.SetFloat("_str", str);
            yield return null;
        }
        //rend.material.SetFloat("_str", str);
    }
}
