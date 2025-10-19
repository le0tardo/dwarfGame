using UnityEngine;

public class LanternPush : MonoBehaviour
{
    [SerializeField] Rigidbody lastJoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("lantern pushed by player");
            Vector3 p= other.transform.position;
            CharacterController c = other.GetComponentInParent<CharacterController>();
            float cm=c.velocity.magnitude;
            PushLantern(p,cm);
        }
    }

    void PushLantern(Vector3 playerPos, float force)
    {
        float f = 0.005f*force/4;
        Vector3 v=new Vector3(f,f,f);
        //lastJoint.AddForce(v,ForceMode.Impulse);
        lastJoint.AddForceAtPosition(v,playerPos,ForceMode.Impulse);
    }
}
