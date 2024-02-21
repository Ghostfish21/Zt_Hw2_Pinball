using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour {
    public float speed;
    private float rotation = 0;
    private Rigidbody2D r2d;

    private static float generateXStart = -3.45f;
    private static float generateXEnd = 3.45f;
    private static float generateYStart = 6.42f;
    private static float generateYEnd = 1f;
    
    // Start is called before the first frame update
    void Start() {
        r2d = GetComponent<Rigidbody2D>();
        float speedLimit = (float)new System.Random().NextDouble() * 1000f;
        float speedLimit1 = (float)new System.Random().NextDouble() * 1000f;
        float length = (float)new System.Random().NextDouble() * 2.3f;
        float width = (float)new System.Random().NextDouble() * 0.8f;
        
        float random = (float)new System.Random().NextDouble();
        float x = lerp(generateXStart, generateXEnd, random);
        float random1 = (float)new System.Random().NextDouble();
        float y = lerp(generateYStart, generateYEnd, random1);
        
        speed = (float) new System.Random().NextDouble() * (speedLimit1 + speedLimit);
        speed -= speedLimit1;
        
        transform.localScale = new Vector3(length, width, 1);
        transform.position = new Vector3(x, y, 0);
    }
    
    // Update is called once per frame
    void Update() {
        rotation += speed * Time.deltaTime;
        rotation %= 360;
        r2d.MoveRotation(rotation);
    }
    
    private float lerp(float a, float b, float t) {
        return a + (b - a) * t;
    }
    
    public void setLifetime(float fallingBallTime) {
        StartCoroutine(destroyAfter(fallingBallTime));
    }

    private IEnumerator destroyAfter(float time) {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
