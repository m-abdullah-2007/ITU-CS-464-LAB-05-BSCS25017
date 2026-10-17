using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Transform player;
    [SerializeField] float interval = 1.5f;
    [SerializeField] int maxCoins = 5;
    [SerializeField] float area = 7f;

    void Start() => InvokeRepeating(nameof(SpawnCoin), 1f, interval);

    void SpawnCoin()
    {
        if (transform.childCount >= maxCoins) return;
        float x = Random.Range(-area, area);
        float z = Random.Range(-area, area);
        Vector3 pos = new Vector3(x, 1f, z);
        if (Vector3.Distance(pos, player.position) < 2f) return;
        // Keep the prefab's own rotation (the 90,0,0 that stands the coin up)
        Instantiate(coinPrefab, pos, coinPrefab.transform.rotation, transform);
    }
}