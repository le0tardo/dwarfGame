using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OreShatter : MonoBehaviour
{
    [SerializeField] GameObject intactRock;
    [SerializeField] GameObject shatteredRock;
    [SerializeField] public List<GameObject> shards = new List<GameObject>();

    private void Awake()
    {
        shards.Clear();

        foreach (Transform child in shatteredRock.transform)
        {
            shards.Add(child.gameObject);
        }
    }
    private void OnEnable()
    {
        ShatterRock(Vector3.zero);
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
         //   Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        //}
    }
    public void ShatterRock(Vector3 shatterPoint)
    {
        foreach (GameObject shard in shards)
        {
            Rigidbody rb = shard.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 dir = (shard.transform.position - shatterPoint).normalized;
                rb.AddForce(dir * 5f, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
            }
        }
    }
}
