using System;
using System.Collections;
using Modules.Tween.Scripts;
using UnityEngine;

namespace DefaultNamespace {
    public class Stick : MonoBehaviour {
        private Rigidbody2D r2d;
        
        private void Start() {
            r2d = GetComponent<Rigidbody2D>();
            registerDownTween();
            StartCoroutine(delayedUpTween(0.1f));
        }
        
        private IEnumerator delayedUpTween(float seconds) {
            yield return new WaitForSeconds(seconds);
            registerUpTween();
        }

        public KeyCode keyCode;

        public float rotationStart;
        public float rotationEnd;
        
        private float rotation = 0f;
        private TweenBuilder onRotationTween;
        private TweenBuilder offRotationTween;
        
        public AudioCollectionPlayer slideSounds;

        private void Update() {
            if (Input.GetKeyDown(keyCode)) {
                slideSounds.playRandom();
                registerDownTween();
            }
            if (Input.GetKeyUp(keyCode)) {
                slideSounds.playRandom();
                registerUpTween();
            }
        }

        private void registerDownTween() {
            TweenBuilder rotationTween = new TweenBuilder().setProperties($@"
                    start-value: {rotation};
                    end-value: {rotationEnd};
                    duration: 0.1;
                    ease: InOutSine;
                ");
            this.onRotationTween = rotationTween;
            rotationTween.setSetter(x => {
                if (onRotationTween == null) return;
                rotation = x;
                r2d.MoveRotation(rotation);
            });
            offRotationTween = null;
            rotationTween.register<float>();
        }

        private void registerUpTween() {
            TweenBuilder rotationTween = new TweenBuilder().setProperties($@"
                    start-value: {rotation};
                    end-value: {rotationStart};
                    duration: 0.1;
                    ease: InOutSine;
                ");
            this.offRotationTween = rotationTween;
            rotationTween.setSetter(x => {
                if(offRotationTween == null) return;
                rotation = x;
                r2d.MoveRotation(rotation);
            });
            onRotationTween = null;
            rotationTween.register<float>();
        }
        
        public AudioCollectionPlayer hitSounds;

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag("Bouncing Ball")) {
                hitSounds.playRandom();
            }
        }
    }
}