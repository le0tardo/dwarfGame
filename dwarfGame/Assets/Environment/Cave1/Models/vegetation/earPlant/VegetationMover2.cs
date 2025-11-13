using UnityEngine;

public class VegetationMover2 : MonoBehaviour
{

    [SerializeField] bool playerHit = false;
    [SerializeField] bool playerMove = false;
    [SerializeField] float str = 0;
    [SerializeField] float maxStr = 0.25f;
    MeshRenderer rend;
    CharacterController c;


    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        rend.material.SetFloat("_str", 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHit = true;
            c=other.GetComponentInParent<CharacterController>();
            if(c!=null)Debug.Log("got char ctrl");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerHit= false;
            c = null;
        }
    }

    private void Update()
    {
        if ((c != null) && (c.velocity.magnitude > 0))
        {
                playerMove = true;
        }
        else
        {
            playerMove= false;
        }

        if (playerMove)
        {
            if (str < maxStr) { str+=0.01f;}
        }
        else
        {
            if (str > 0) { str -= 0.01f; }
        }

        rend.material.SetFloat("_str", str);
    }
}
