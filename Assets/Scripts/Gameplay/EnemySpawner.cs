using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnCooldown = 1.0f;
    private bool _canSpawn = true;
    public List<GameObject> enemyList;

    private void FixedUpdate() {
        if(_canSpawn) {
            int randomIndex = Random.Range(0, enemyList.Count - 1);
            Instantiate(enemyList[randomIndex], transform);
            StartCoroutine(SpawnDelay());
        }
    }

    IEnumerator SpawnDelay() {
        _canSpawn = false;
        yield return new WaitForSeconds(spawnCooldown);
        _canSpawn = true;
    }
}
