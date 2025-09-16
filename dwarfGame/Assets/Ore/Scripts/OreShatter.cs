using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OreShatter : MonoBehaviour
{
    [SerializeField] GameObject intactRock;
    [SerializeField] GameObject shatteredRock;
    [SerializeField] GameObject mineral;
    [SerializeField] GameObject particle;
    [SerializeField] public List<GameObject> shards = new List<GameObject>();
    [SerializeField] public List<GameObject> minerals = new List<GameObject>();

    private void Awake()
    {
        shards.Clear();

        foreach (Transform child in shatteredRock.transform)
        {
            shards.Add(child.gameObject);
        }
        shatteredRock.SetActive(false);

        minerals.Clear();

        foreach(Transform child in mineral.transform)
        {
            minerals.Add(child.gameObject);
        }
    }
    private void OnEnable()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShatterRock(Vector3.zero);
        }
    }
    public void ShatterRock(Vector3 shatterPoint)
    {
        Debug.Log("Nu ska stenen gå sönder john!");
        intactRock.SetActive(false);
        shatteredRock.SetActive(true);
        particle.SetActive(true);

        foreach (GameObject shard in shards)
        {
            Rigidbody rb = shard.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 dir = (shard.transform.position - shatterPoint).normalized;
                rb.AddForce(dir * 5f, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
            }
            StartCoroutine(ShrinkAllShards(0.66f)); // 1f = shrink duration in seconds
        }
        foreach(GameObject mineral in minerals)
        {
            Rigidbody rb= mineral.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }

    private IEnumerator ShrinkAllShards(float duration)
    {
        float elapsed = 0f;
        List<Vector3> originalScales = new List<Vector3>();
        foreach (var shard in shards)
            originalScales.Add(shard.transform.localScale);

        while (elapsed < duration)
        {
            for (int i = 0; i < shards.Count; i++)
            {
                if (shards[i] != null)
                {
                    shards[i].transform.localScale = Vector3.Lerp(originalScales[i], Vector3.zero, elapsed / duration);
                }
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        // snap all to zero
        foreach (var shard in shards)
            if (shard != null) shard.transform.localScale = Vector3.zero;

        // finally, deactivate parent
        shatteredRock.gameObject.SetActive(false);
    }

}
