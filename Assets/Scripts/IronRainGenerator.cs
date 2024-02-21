using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class IronRainGenerator : MonoBehaviour {
    public GameObject ironRainPrefab;
    
    private static float generateXStart = -3.45f;
    private static float generateXEnd = 3.45f;
    private static float generateYStart = 7.5f;
    
    public List<GameObject> inactiveIronRain = new();
    public List<GameObject> activeIronRain = new();
    private int amountPerTick;

    private float lifetime = 5;
    
    public float frameGenerateChance = 0;
    public int rainPerGenerate = 3;
    private float secondPerFrame = 0.25f;

    private void FixedUpdate() {
        float r = (float) new System.Random().NextDouble();
        if (r < frameGenerateChance) {
            generateIronRain();
        }
    }

    private void createNewRain() {
        GameObject ironRainInstance = Instantiate(ironRainPrefab, transform);
        ironRainInstance.GetComponent<IronRain>().setGenerator(this);
        inactiveIronRain.Add(ironRainInstance);
    }
    
    private void generateIronRain() {
        for (int i = 0; i < rainPerGenerate; i++) {
            float x = lerp(generateXStart, generateXEnd, (float) new System.Random().NextDouble());
            if (inactiveIronRain.Count == 0) createNewRain();
            GameObject ironRainInstance = inactiveIronRain[0];
            
            float length = (float) new System.Random().NextDouble() * 0.4f + 0.4f;
            float width = 0.4f + (float) new System.Random().NextDouble() * 0.2f;
            ironRainInstance.transform.localScale = new Vector3(length, width, 1);
            
            float direction = (float) new System.Random().NextDouble() * 4 + 90;
            ironRainInstance.GetComponent<Rigidbody2D>().MoveRotation(direction);
            ironRainInstance.GetComponent<Rigidbody2D>().drag = 0;
            
            ironRainInstance.transform.position = new Vector3(x, generateYStart, 0);
            ironRainInstance.SetActive(true);
            activeIronRain.Add(ironRainInstance);
            inactiveIronRain.RemoveAt(0);

            StartCoroutine(killRain(ironRainInstance));
        }
    }
    
    private float lerp(float a, float b, float t) {
        return a + (b - a) * t;
    }

    private IEnumerator killRain(GameObject rain) {
        yield return new WaitForSeconds(lifetime);
        rain.GetComponent<IronRain>().kill();
    }
    
    public void setGenerateChance(float frameGenerateChance, float duration) {
        this.frameGenerateChance = frameGenerateChance;
        StartCoroutine(stopRaining(duration));
    }

    private IEnumerator stopRaining(float duration) {
        yield return new WaitForSeconds(duration);
        frameGenerateChance = 0;
    } 
}
