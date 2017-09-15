using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float mousePosInBlocks = Input.mousePosition.x / Screen.width * 16;
        //print(mousePosInBlocks);
        Vector3 paddleCoords = new Vector3(Mathf.Clamp(mousePosInBlocks,0.5f,15.5f), this.transform.position.y, this.transform.position.z);
        this.transform.position = paddleCoords;
	}
}
