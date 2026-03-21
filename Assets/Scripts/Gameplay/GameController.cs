using UnityEngine.InputSystem;
using UnityEngine;
using Core;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation
namespace Gameplay {
    public class GameController : MonoBehaviour {
        [SerializeField] private GameObject[] hearts;
        [SerializeField] private GameObject pauseScreen;
        [SerializeField] private GameObject deathScreen;
        [SerializeField] private GameObject mainScreen;

        private GameManager _logic;

        public static GameController Instance { get; private set; }

        private void Awake() {
            if (Instance && Instance != this) { Destroy(gameObject); return; }
            Instance = this;

            _logic = new GameManager();
            PlayerController.Instance.Player.SetMaxLives(hearts.Length);
        }

        private void Start() {
            GoToMainMenu();
        }

        private void Update() {
            if (Keyboard.current.escapeKey.wasPressedThisFrame) {
                if (mainScreen.activeSelf) QuitGame();
                else GoToMainMenu();
            }

            if (Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.pKey.wasPressedThisFrame) TogglePause();
        }

        public void LoseLife() {
            int currentLives = PlayerController.Instance.Player.Lives;
            if (currentLives > 0) {
                PlayerController.Instance.Player.LoseLife();
                PlayerController.Instance.GetComponent<HitFlash>()?.Flash();
                currentLives = PlayerController.Instance.Player.Lives;
                AudioManager.Instance.PlayLoseLife();

                CanvasGroup cg = hearts[currentLives].GetComponent<CanvasGroup>();
                if (cg) cg.alpha = 0;                                           // Hide life
                else hearts[currentLives].SetActive(false);                     // Back up

                if (currentLives <= 0) GameOver();
            }
        }

        public void GoToMainMenu() {
            pauseScreen.SetActive(false);
            deathScreen.SetActive(false);
            mainScreen.SetActive(true);
            Time.timeScale = 0;
            AudioManager.Instance.PlayMusic(AudioManager.Instance.backgroundMusic);
        }

        private void TogglePause() {
            _logic.IsPaused = !_logic.IsPaused;
            Time.timeScale = _logic.IsPaused ? 0 : 1;
            pauseScreen.SetActive(_logic.IsPaused);
        }

        public void Restart() {
            if (_logic.IsGameOver) AudioManager.Instance.PlayMusic(AudioManager.Instance.backgroundMusic);
            _logic.IsGameOver = false;
            PlayerController.Instance.Player.Reset();
            EnemySpawner.Instance.Reset();
            ScoreController.Instance.Reset();
            CleanScene();
            ShowAllLives();
            pauseScreen.SetActive(false);
            deathScreen.SetActive(false);
            mainScreen.SetActive(false);
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

        private void GameOver() {
            _logic.TriggerGameOver();
            AudioManager.Instance.PlayMusic(AudioManager.Instance.gameOverMusic);
            ScoreController.Instance.Save();
            deathScreen.SetActive(true);
        }

        public void QuitGame() {
            ScoreController.Instance.Save();
            Application.Quit();
        }

        private void OnDestroy() {
            if (Instance == this) Instance = null;
        }
    }
}
