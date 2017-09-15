using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour {
    public int maxHits;
    private int hits;
	// Use this for initialization
	void Start () {
        hits = 0;
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        hits++;
        /*if(hits >= maxHits)
        {
            
        }*/
    }
    // Update is called once per frame
    void Update () {
		
	}
}
