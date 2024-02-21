using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class MiscMethods : MonoBehaviour {
    public void decreaseChanceOfAddBall() {
        RandomDrawer rd = GetComponent<RandomDrawer>();
        if (rd.randomResultProfiles[0].percentage < 1f) 
            rd.randomResultProfiles[0].percentage += 0.015f;
        if (rd.randomResultProfiles[1].percentage > 0f)
            rd.randomResultProfiles[1].percentage -= 0.015f;
        rd.update();
    }

    public GameObject rotationObstaclePrefab;
    public GameObject deadZonePrefab;
    public GameObject fruitPrefab;
    public FallingBall fallingBall;
    public void generateNewRotationObstacle() {
        GameObject spawnableRoot = GameObject.Find("Random Layout");
        if (spawnableRoot == null) return;
        GameObject rotationObstacle = Instantiate(rotationObstaclePrefab, spawnableRoot.transform);
        rotationObstacle.GetComponent<RotatingObstacle>().setLifetime(fallingBall.time);
    }

    public void generateNewFruit() {
        GameObject spawnableRoot = GameObject.Find("Random Layout");
        if (spawnableRoot == null) return;
        GameObject fruit = Instantiate(fruitPrefab, spawnableRoot.transform);
        fruit.GetComponent<Fruit>().setLifetime(fallingBall.time);
    }

    public void generateNewDeadZone() {
        GameObject spawnableRoot = GameObject.Find("Random Layout");
        if (spawnableRoot == null) return;
        GameObject deadZone = Instantiate(deadZonePrefab, spawnableRoot.transform);
        deadZone.GetComponent<DeadZone>().setLifetime(fallingBall.time);
    }

    public void startRaining() {
        GameObject rainingGenerator = GameObject.Find("Iron Rain Generator");
        rainingGenerator.GetComponent<IronRainGenerator>().setGenerateChance(0.25f, fallingBall.time * 2);
    }

    public TMP_Text endScore;
    public TMP_Text endUsername;
    public void showFinalScore() {
        endScore.text = $"Score: {Score.getScore(Utility.playerName)}";
        Score.setScore(Utility.playerName, 0);
    }

    public void showFinalPlayerName() {
        endUsername.text = $"{Utility.playerName}";
    }
}
