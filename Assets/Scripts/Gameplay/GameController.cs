using UnityEngine;
using Core;

namespace Gameplay {
    public class GameController : MonoBehaviour {
        [SerializeField] private GameObject[] hearts;
        [SerializeField] private GameObject deathScreenPanel;

        private GameManager _logic;

        public static GameController Instance { get; private set; }

        private void Awake() {
            if (Instance && Instance != this) { Destroy(gameObject); return; }
            Instance = this;

            _logic = new GameManager();
            PlayerController.Instance.Player.SetMaxLives(hearts.Length);
            deathScreenPanel.SetActive(false);
        }

        public void LoseLife() {
            int currentLives = PlayerController.Instance.Player.Lives;
            if (currentLives > 0) {
                PlayerController.Instance.Player.LoseLife();
                currentLives = PlayerController.Instance.Player.Lives;

                CanvasGroup cg = hearts[currentLives].GetComponent<CanvasGroup>();
                if (cg) cg.alpha = 0;                                           // Hide life
                else hearts[currentLives].SetActive(false);                     // Back up

                if (currentLives <= 0) GameOver();
            }
        }

        private void GameOver() {                                               // L Show death screen
            _logic.TriggerGameOver();
            ScoreController.Instance.Save();
            deathScreenPanel.SetActive(true);
        }

        public void Restart() {
            _logic.IsGameOver = false;
            PlayerController.Instance.Player.Reset();
            EnemySpawner.Instance.Reset();
            ScoreController.Instance.Reset();
            CleanScene();
            ShowAllLives();
            deathScreenPanel.SetActive(false);
            Time.timeScale = 1;
        }

        private void CleanScene() {
            EnemyController[] enemies = FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
            foreach (EnemyController e in enemies) Destroy(e.gameObject);       // Delete all enemies

            BulletController[] bullets = FindObjectsByType<BulletController>(FindObjectsSortMode.None);
            foreach (BulletController b in bullets) Destroy(b.gameObject);      // Delete all bullets
        }

        private void ShowAllLives() {
            foreach (var heart in hearts) {
                CanvasGroup cg = heart.GetComponent<CanvasGroup>();
                if (cg) cg.alpha = 1;                                           // Show life
                else heart.SetActive(true);                                     // Back up
            }
        }
        
        private void OnDestroy() {
            if (Instance == this) Instance = null;
        }
    }
}
