using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Paddle paddle;
    private Vector3 paddleToBallVector;
    private bool gameStart = false;
    Rigidbody2D ballComponent;
	// Use this for initialization
	void Start () {
        paddle = FindObjectOfType<Paddle>();
        paddleToBallVector = this.transform.position - paddle.transform.position;
        ballComponent = GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update() {
        if (!gameStart)
        {
            this.transform.position = paddle.transform.position + paddleToBallVector;
        
            if (Input.GetMouseButtonDown(0))
            {
                print("Mouse button pressed.");
                ballComponent.velocity = new Vector2(2, 10);
                gameStart = true;
        }
        }
	}
}
