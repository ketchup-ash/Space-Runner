using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] prefab;
    public float delay = 5f;
    public int maximum = 12;

    public int minDist = 13;
    public int maxDist = 16;

    private List<GameObject> list = new List<GameObject>();
    private float m_internalTimer = 5f;
    void Start() {
        m_internalTimer = delay;
    }
    void Update() {
        if (list.Count >= maximum)
            return;

        m_internalTimer -= Time.deltaTime;
        m_internalTimer = Mathf.Max(m_internalTimer, 0f);
        if (m_internalTimer == 0f) {
            int index = Random.Range(0, prefab.Length);
            Vector3 offset = GetOffset();
            GameObject obj = Instantiate(prefab[index], transform.position + offset, Quaternion.identity) as GameObject;
            list.Add(obj);
            m_internalTimer = delay;
        }
    }

    void LateUpdate() {
        list.RemoveAll(o => (o == null || o.Equals(null)));
    }

    Vector3 GetOffset() {
        Vector3 pos = new Vector3(Random.value - 0.5f, Random.value - 0.5f, 0).normalized * Random.Range(minDist, maxDist);
        return pos;
    }

}
