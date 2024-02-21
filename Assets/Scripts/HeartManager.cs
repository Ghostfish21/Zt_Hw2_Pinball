using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace {
    public class HeartManager : MonoBehaviour {
        public UnityEvent onHeartCleared;
        
        private int heart = 3;

        public GameObject heart1;
        public GameObject heart2;
        public GameObject heart3;

        public void start() {
            heart = 3;
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
        }
        
        public void addHeart() {
            heart++;
            if (heart == 3) {
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(true);
            } else if (heart == 2) {
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(false);
            } else if (heart == 1) {
                heart1.SetActive(true);
                heart2.SetActive(false);
                heart3.SetActive(false);
            }
        }
        
        public void removeHeart() {
            heart--;
            if (heart == 2) {
                heart1.SetActive(true);
                heart2.SetActive(true);
                heart3.SetActive(false);
            } else if (heart == 1) {
                heart1.SetActive(true);
                heart2.SetActive(false);
                heart3.SetActive(false);
            } else if (heart == 0) {
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
                onHeartCleared.Invoke();
            }
        }
    }
}