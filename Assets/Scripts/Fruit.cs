using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Fruit : MonoBehaviour {
    
    private static float generateXStart = -3.45f;
    private static float generateXEnd = 3.45f;
    private static float generateYStart = 6.42f;
    private static float generateYEnd = 1f;
    
    private void Start() {
        float random = (float)new System.Random().NextDouble();
        float x = lerp(generateXStart, generateXEnd, random);
        float random1 = (float)new System.Random().NextDouble();
        float y = lerp(generateYStart, generateYEnd, random1);
        transform.position = new Vector3(x, y, 0);
    }

    private float lerp(float a, float b, float t) {
        return a + (b - a) * t;
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Bouncing Ball")) {
            Destroy(gameObject);
            Score.addScore(Utility.playerName, 15);
        }
    }
    
    public void setLifetime(float lifetime) {
        StartCoroutine(destroyAfter(lifetime));
    }

    private IEnumerator destroyAfter(float time) {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
