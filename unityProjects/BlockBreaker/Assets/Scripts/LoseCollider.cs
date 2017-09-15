using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour {

    private LevelManager manager;
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        manager = FindObjectOfType<LevelManager>();
        manager.loadLevel("Lose");
        print("Trigger.");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Collision.");
    }
}
