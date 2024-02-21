using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace {
    public class BouncingBallPool : MonoBehaviour {
        private List<BouncingBall> inactivePool = new List<BouncingBall>();
        private List<BouncingBall> activePool = new List<BouncingBall>();
        private Transform ballRoot;

        private void Start() {
            ballRoot = GameObject.Find("Bouncing Balls").transform;
            for (int i = 0; i < 5; i++) createNewBall();
            activeBall();
        }
        
        private void createNewBall() {
            BouncingBall ball = Instantiate(Resources.Load<BouncingBall>("Bouncing Ball"), ballRoot);
            ball.gameObject.SetActive(false);
            inactivePool.Add(ball);
        }

        private void activeBallAt(Vector3 position) {
            if (inactivePool.Count == 0) createNewBall();
            BouncingBall ball = inactivePool[0];
            inactivePool.RemoveAt(0);
            activePool.Add(ball);
            ball.transform.position = position;
            ball.gameObject.SetActive(true);
        }

        public void activeBall() {
            try {
                BallSpawner ballSpawner = GameObject.Find("Ball Spawner").GetComponent<BallSpawner>();
                activeBallAt(ballSpawner.getSpawnPosition());
            }
            catch (Exception e) {
                // ignored
            }
        }
        
        public void inactiveBall(BouncingBall ball) {
            activePool.Remove(ball);
            inactivePool.Add(ball);
            ball.gameObject.SetActive(false);
        }

        public int activeBallCount() {
            return activePool.Count;
        }
    }
}