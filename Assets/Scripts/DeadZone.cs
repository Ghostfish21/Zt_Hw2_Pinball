using System;
using System.Collections;
using System.Collections.Generic;
using Modules.Tween.Scripts;
using UnityEngine;

public class DeadZone : MonoBehaviour {

    private static float generateXStart = -3.45f;
    private static float generateXEnd = 3.45f;
    private static float generateYStart = 6.42f;
    private static float generateYEnd = 1f;
    
    private float startSize;
    private float endSize;
    private float duration;

    private Vector2 randomForceDirection;

    private TweenBuilder tweenAwaitForRegister;
    
    private void Start() {
        startSize = (float)new System.Random().NextDouble() * 1f;
        endSize = (float)new System.Random().NextDouble() * 3f;
        duration = (float)new System.Random().NextDouble() * 50f;
        
        float random = (float)new System.Random().NextDouble();
        float x = lerp(generateXStart, generateXEnd, random);
        float random1 = (float)new System.Random().NextDouble();
        float y = lerp(generateYStart, generateYEnd, random1);
        transform.position = new Vector3(x, y, 0);

        randomForceDirection =
            new Vector2((float)new System.Random().NextDouble(), (float)new System.Random().NextDouble());
        randomForceDirection = randomForceDirection.normalized;
        
        constructPositiveTween();
    }

    private float lerp(float a, float b, float t) {
        return a + (b - a) * t;
    }

    public void setLifetime(float lifetime) {
        StartCoroutine(destroyAfter(lifetime));
    }

    private IEnumerator destroyAfter(float time) {
        time *= 10;
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
    
    private void FixedUpdate() {
        if (tweenAwaitForRegister == null) return;
        tweenAwaitForRegister.register<float>();
        tweenAwaitForRegister = null;
    }
    
    private void constructPositiveTween() {
        TweenBuilder tween = new TweenBuilder().setProperties($@"
                    start-value: {startSize};
                    end-value: {endSize};
                    duration: {duration};
                    ease: InOutSine;
                ").setSetter(x => {
                try {
                    transform.localScale = new Vector3(x, x, x);
                }
                catch (Exception e) {
                    // ignored
                }
            })
            .setOnComplete(constructNegativeTween);
        tweenAwaitForRegister = tween;
    }
    
    private void constructNegativeTween() {
        TweenBuilder tween = new TweenBuilder().setProperties($@"
                    start-value: {endSize};
                    end-value: {startSize};
                    duration: {duration};
                    ease: InOutSine;
                ").setSetter(x => {
                try {
                    transform.localScale = new Vector3(x, x, x);
                }
                catch (Exception e) {
                    // ignored
                }
            })
            .setOnComplete(constructPositiveTween);
        tweenAwaitForRegister = tween;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Bouncing Ball")) {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Bouncing Ball")) {
            other.GetComponent<Rigidbody2D>().AddForce(randomForceDirection);
        }
    }
}
