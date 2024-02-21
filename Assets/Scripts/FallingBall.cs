using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FallingBall : MonoBehaviour {
    public RandomDrawer randomDrawer;
    private Rigidbody2D r2d;

    public float time { get; private set; } = 5f;

    private void Start() {
        r2d = GetComponent<Rigidbody2D>();
    }

    public void reset() {
        // Debug.Log($"{randomDrawer.leftBoundX}£¬ {randomDrawer.rightBoundX}");
        float position = lerp(randomDrawer.leftBoundX, randomDrawer.rightBoundX, (float)new System.Random().NextDouble());
        transform.position = new Vector3(position, 5.96f, 0);
        Vector2 force = new Vector2((float)new System.Random().NextDouble() * 100f, 
            (float)new System.Random().NextDouble() * 100f);
        r2d.AddForce(force);
    }

    public void doubleTime() {
        time *= 2;
    }

    public void resetTime() {
        time = 5;
    }

    private float lerp(float a, float b, float t) {
        return a + (b - a) * t;
    }
}
