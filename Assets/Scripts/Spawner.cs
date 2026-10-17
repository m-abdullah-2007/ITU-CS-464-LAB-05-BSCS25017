using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject coinPrefab;
    [SerializeField] Transform player;
    [SerializeField] float interval = 1.5f;
    [SerializeField] int maxCoins = 5;
    [SerializeField] float area = 7f;

    [Header("Bonus ring")]
    [SerializeField] float bonusMinRadius = 2f;     // keeps blue coins off the player
    [SerializeField] float bonusMaxRadius = 7f;
    [SerializeField] float arenaHalfSize = 9.5f;    // Arena plane is 20x20, so edges are at 10

    void Start() => InvokeRepeating(nameof(SpawnCoin), 1f, interval);

    // Normal coins: children of the Spawner, so childCount caps them
    void SpawnCoin()
    {
        if (transform.childCount >= maxCoins) return;
        float x = Random.Range(-area, area);
        float z = Random.Range(-area, area);
        Vector3 pos = new Vector3(x, 0.5f, z);
        if (Vector3.Distance(pos, player.position) < 2f) return;
        Spawn(pos, CoinType.Normal, transform);
    }

    // Task 1: one red coin, somewhere in the arena, away from the player
    public void SpawnRedCoin()
    {
        Vector3 pos = Vector3.zero;
        for (int i = 0; i < 20; i++)
        {
            pos = new Vector3(Random.Range(-area, area), 0.5f, Random.Range(-area, area));
            if (Vector3.Distance(pos, player.position) >= 2f) break;
        }
        Spawn(pos, CoinType.Red, null);   // null parent: not counted in maxCoins
    }

    // Task 2: 'count' blue coins on a disc around 'center' (the player)
    public void SpawnBonusCoins(Vector3 center, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = center;
            for (int tries = 0; tries < 30; tries++)
            {
                // sqrt makes the coins evenly spread over the ring, not bunched at the centre
                float r = Mathf.Sqrt(Random.Range(bonusMinRadius * bonusMinRadius,
                                                  bonusMaxRadius * bonusMaxRadius));
                float angle = Random.Range(0f, Mathf.PI * 2f);
                pos = new Vector3(center.x + Mathf.Cos(angle) * r, 0.5f,
                                  center.z + Mathf.Sin(angle) * r);
                if (Mathf.Abs(pos.x) <= arenaHalfSize && Mathf.Abs(pos.z) <= arenaHalfSize) break;
            }
            pos.x = Mathf.Clamp(pos.x, -arenaHalfSize, arenaHalfSize);
            pos.z = Mathf.Clamp(pos.z, -arenaHalfSize, arenaHalfSize);
            Spawn(pos, CoinType.Blue, null);
        }
    }

    void Spawn(Vector3 pos, CoinType type, Transform parent)
    {
        GameObject go = Instantiate(coinPrefab, pos, coinPrefab.transform.rotation, parent);
        go.GetComponent<Coin>().Init(type);
    }
}