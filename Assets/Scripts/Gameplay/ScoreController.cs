using UnityEngine.UI;
using UnityEngine;
using Core;

namespace Gameplay {
    public class ScoreController : MonoBehaviour {
        [SerializeField] private Text scoreText;
        
        private ScoreCalculator _calculator;
        private int _killCount;

        public static ScoreController Instance { get; private set; }

        void Awake() {
            if (Instance && Instance != this) { Destroy(gameObject); return; }
            Instance = this;

            _calculator = new ScoreCalculator();
            UpdateUI();
        }

        public void AddKill(int score) {
            _killCount++;
            PlayerController.Instance.Player.AddScore(_calculator.Calculate(score));
            _calculator.ApplyCombo(1);

            UpdateUI();
        }

        private void UpdateUI() {
            int score = PlayerController.Instance.Player.Score;
            if (scoreText) scoreText.text = $"SCORE: {score} (x{_killCount})";
        }

        public void Reset() {
            _killCount = 0;
            _calculator.ResetMultiplier();
            UpdateUI();
        }
        
        private void OnDestroy() {
            if (Instance == this) Instance = null;
        }
    }
}
