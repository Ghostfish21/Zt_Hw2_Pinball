using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace {
    public class BouncingBall : MonoBehaviour {
        private void Start() {
            TMP_Text scoreText = GameObject.Find("Score Text").GetComponent<TMP_Text>();
            scoreText.text = $"Score: {Score.getScore(Utility.playerName)}";
            
            TMP_Text playerNameText = GameObject.Find("Player Name").GetComponent<TMP_Text>();
            playerNameText.text = $"{Utility.playerName}";
        }
    }
}