using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class IronRain : MonoBehaviour {
    private Rigidbody2D r2d;
    
    // Start is called before the first frame update
    void Start() {
        r2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        float speed = 50 * Time.deltaTime;
        r2d.MovePosition(new Vector2(r2d.position.x + -transform.right.x * speed, r2d.position.y + -transform.right.y * speed));
    }

    private IronRainGenerator generator;
    public void setGenerator(IronRainGenerator g) {
        generator = g;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Bouncing Ball")) {
            BouncingBall bb = other.gameObject.GetComponent<BouncingBall>();
            bb.GetComponent<Rigidbody2D>().velocity *= 0.35f;
            kill();
        }
    }

    public void kill() {
        gameObject.SetActive(false);
        generator.activeIronRain.Remove(gameObject);
        generator.inactiveIronRain.Add(gameObject);
    }
}
