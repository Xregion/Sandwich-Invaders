using UnityEngine;
using System;

public class EnemyRespawn : MonoBehaviour {

    public event Action respawnEvent;

    EnemyMovement[] enemies;
    int enemiesRemaining;

	void Start () {
        enemies = GetComponentsInChildren<EnemyMovement> ();
        enemiesRemaining = enemies.Length;
        for (int i = 0; i < enemies.Length; i++) {
            enemies[i].hitEvent += CheckEnemyCount;
        }
    }

    void CheckEnemyCount () {
        enemiesRemaining -= 1;
        if (enemiesRemaining <= 0) {
            enemiesRemaining = enemies.Length - 1;
            if (respawnEvent != null) {
                respawnEvent ();
            }
        }
    }
}
