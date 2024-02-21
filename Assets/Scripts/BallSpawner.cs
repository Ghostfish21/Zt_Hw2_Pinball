using System;
using System.Collections;
using System.Collections.Generic;
using Modules.Tween.Scripts;
using UnityEngine;

public class BallSpawner : MonoBehaviour {
    public float startX;
    public float endX;

    private TweenBuilder tweenAwaitForRegister;

    private void Start() {
        constructPositiveTween();
    }

    private void FixedUpdate() {
        if (tweenAwaitForRegister == null) return;
        tweenAwaitForRegister.register<float>();
        tweenAwaitForRegister = null;
    }

    public Vector3 getSpawnPosition() {
        return new Vector3(transform.position.x, 5.89f, 0f);
    }

    private void constructPositiveTween() {
        TweenBuilder tween = new TweenBuilder().setProperties($@"
                    start-value: {startX};
                    end-value: {endX};
                    duration: 2.5;
                    ease: InOutSine;
                ").setSetter(x => { transform.position = new Vector3(x, transform.position.y);})
            .setOnComplete(constructNegativeTween);
        tweenAwaitForRegister = tween;
    }
    
    private void constructNegativeTween() {
        TweenBuilder tween = new TweenBuilder().setProperties($@"
                    start-value: {endX};
                    end-value: {startX};
                    duration: 2.5;
                    ease: InOutSine;
                ").setSetter(x => { transform.position = new Vector3(x, transform.position.y); })
            .setOnComplete(constructPositiveTween);
        tweenAwaitForRegister = tween;
    }
}
