using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class VegetationMover : MonoBehaviour
{
    MeshRenderer rend;
    float d = 0.25f;
    [SerializeField] float str = 0f;
    bool move=false;
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

    private void Update()
    {
        if (move)
        {
            rend.material.SetFloat("_str", str);
        }
    }

    IEnumerator MoveVegetation()
    {
        move=true;
        float e = 0f;
        float s = 0f;
        float t = 0.25f;

        while(e < d)
        {
            e += Time.deltaTime;
            str = Mathf.Lerp(s, t, e / d);
            yield return null;
        }

        str = t;

        e=0f;
        while(e < d)
        {
            e+= Time.deltaTime;
            str = Mathf.Lerp(t, s, e / d);
            yield return null;
        }
        str=0f;
        move= false;    
    }
}
