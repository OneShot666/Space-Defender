using UnityEngine.UI;
using UnityEngine;
using Core;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation
namespace Gameplay {
    public class ScoreController : MonoBehaviour {
        [Header("Combo settings")]
        [SerializeField, Range(0, 1)] private float comboBonus = 0.2f;
        [SerializeField, Range(0, 1)] private float comboDecaySpeed = 0.5f;
        [SerializeField, Range(0.5f, 10)] private float comboDecayTime = 2.0f;
        [SerializeField] private Color minComboColor = Color.white;
        [SerializeField] private Color maxComboColor = Color.purple;

        [Header("References")]
        [SerializeField] private Text scoreText;
        [SerializeField] private Text highScoreText;
        [SerializeField] private Text killText;
        [SerializeField] private Text multText;
        [SerializeField] private Text comboText;

        private const string HighScoreKey = "HighScore";
        private ScoreCalculator _calculator;
        private float _lastKillTime;
        private int _killCount;

        public static ScoreController Instance { get; private set; }

        void Awake() {
            if (Instance && Instance != this) { Destroy(gameObject); return; }
            Instance = this;

            _calculator = new ScoreCalculator();
            UpdateUI();
        }

        void Update() {
            if (Time.time > _lastKillTime + comboDecayTime) {                   // Reduce combo if too slow
                _calculator.DecayCombo(comboDecaySpeed * Time.deltaTime);
                UpdateUI();
            }
        }

        public float GetMultiplier() => _calculator.Multiplier;

        public float GetMinMult() => _calculator.MinMult;

        public float GetMaxMult() => _calculator.MaxMult;

        public void AddKill(int score) {
            _killCount++;
            PlayerController.Instance.Player.AddScore(_calculator.Calculate(score));
            _calculator.IncreaseCombo(comboBonus);
            _lastKillTime = Time.time;

            UpdateUI();
        }

        private void UpdateUI() {
            int score = PlayerController.Instance.Player.Score;
            if (scoreText) scoreText.text = $"Score: {score}";

            if (highScoreText) {
                int best  = PlayerPrefs.GetInt(HighScoreKey);
                highScoreText.gameObject.SetActive(best > 0);
                highScoreText.text = $"Best: {best}";
            }

            if (killText) {
                killText.gameObject.SetActive(_killCount > 0);
                killText.text = $"K I L L S  :  {_killCount}";
            }

            if (multText) {
                float currentMult = _calculator.Multiplier;

                if (currentMult <= _calculator.MinMult + 0.05f) {               // If too close to min
                    multText.enabled = false;
                    if (comboText) comboText.enabled = false;
                } else {
                    multText.enabled = true;
                    multText.text = $"x{currentMult:F1}";
                    if (comboText) comboText.enabled = true;

                    float t = (currentMult - _calculator.MinMult) / (_calculator.MaxMult - _calculator.MinMult); 
                    Color color = Color.Lerp(minComboColor, maxComboColor, t);
                    multText.color = color;
                    multText.transform.localScale = Vector3.one * (1f + t * 0.5f); // Text change size based on value
                    if (comboText) comboText.color = color;
                }
            }
        }

        public void Reset() {
            _killCount = 0;
            _calculator.ResetMultiplier();
            UpdateUI();
        }

        public void Save() {
            int currentScore = PlayerController.Instance.Player.Score;
            int currentBest = PlayerPrefs.GetInt(HighScoreKey, 0);

            if (currentScore > currentBest) {
                PlayerPrefs.SetInt(HighScoreKey, currentScore);
                PlayerPrefs.Save();
            }
        }
        
        private void OnDestroy() {
            if (Instance == this) Instance = null;
        }
    }
}
