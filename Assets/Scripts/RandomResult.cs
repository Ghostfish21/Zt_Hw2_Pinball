using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class RandomResult : MonoBehaviour {
    public bool shouldResetTime;
    public UnityEvent onHit;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bouncing Ball")) {
            BouncingBall ball = other.gameObject.GetComponent<BouncingBall>();
            BouncingBallPool pool = GameObject.Find("Bouncing Ball Pool").GetComponent<BouncingBallPool>();
            pool.inactiveBall(ball);
            
            if (pool.activeBallCount() == 0) {
                HeartManager heartManager = GameObject.Find("Heart Manager").GetComponent<HeartManager>();
                heartManager.removeHeart();
                
                BallSpawner ballSpawner = GameObject.Find("Ball Spawner").GetComponent<BallSpawner>();
                pool.activeBall();
            }
            return;
        }

        if (other.CompareTag("Falling Ball")) {
            FallingBall fallingBall = other.gameObject.GetComponent<FallingBall>();
            fallingBall.reset();
            if (shouldResetTime)
                fallingBall.resetTime();
            onHit?.Invoke();
        }
    }
}
