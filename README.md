# ITU-CS-464-LAB-01-BSCS25017

## Decisions
 
1. **`ForceMode.Force` for player movement**
   Movement is a continuous push applied every `FixedUpdate`, and `Force` accounts for mass. `Impulse` every step would act as 50 kicks per second.
2. **Sphere Collider (trigger) for the coin**
   It is cheap and looks the same however the coin spins. Is Trigger lets the ball pass through and fires `OnTriggerEnter`.
3. **`Destroy(gameObject)`, not `Destroy(this)`**
   `Destroy(this)` only removes the script and leaves a frozen coin. `Destroy(gameObject)` removes the whole coin.
4. **Spawning with `InvokeRepeating` + `Instantiate`**
   It is one line for a repeating timer, and the coin prefab is copied at a random spot. `childCount` on the Spawner caps how many exist.
5. **`Time.timeScale = 1f` in `Restart()`**
   `Win()` sets it to 0, and it survives a scene load, so the new game would start frozen. Reset it before `LoadScene`.
6. **`void Awake() => Instance = this;` (singleton)**
   Any script can reach the one GameManager, and `Awake` runs before every `Start`, so it already exists. One owner means one score and one win check.
7. **Coin rotation and spawn**
   The prefab is rotated (90,0,0) to stand up, and the Spawner uses `coinPrefab.transform.rotation` instead of `Quaternion.identity`. `Rotate(..., Space.World)` spins it around vertical Y, not its own long axis.
8. **Red coin at a random score**
   `Random.Range(1, targetScore)` is picked once in `Start`, giving 1 to 9 (10 would end the game first). A `redSpawned` flag keeps it to one coin.
9. **One prefab with a `CoinType` (Normal, Red, Blue)** 
   Colour and behaviour are set in code, so there are no extra prefabs to maintain.
10. **Blue coins on a ring around the player, worth 0 main score** 
    A square-root radius spreads the 50 coins evenly. Zero score value keeps bonus coins from triggering the win early.
11. **Red and blue coins are not parented to the Spawner** 
    They don't count against `maxCoins`, so normal spawning isn't blocked.
12. **Fixed angled top-down camera**
    The whole arena stays visible, and the world-axis controls still match the screen.
 
