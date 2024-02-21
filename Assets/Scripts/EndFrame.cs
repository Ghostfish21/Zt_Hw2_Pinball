using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;

public class EndFrame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Bouncing Ball")) {
            BouncingBall ball = other.gameObject.GetComponent<BouncingBall>();
            BouncingBallPool pool = GameObject.Find("Bouncing Ball Pool").GetComponent<BouncingBallPool>();
            pool.inactiveBall(ball);
            
            if (pool.activeBallCount() == 0) {
                HeartManager heartManager = GameObject.Find("Heart Manager").GetComponent<HeartManager>();
                heartManager.removeHeart();
                
                pool.activeBall();
            }
        }
    }
}
