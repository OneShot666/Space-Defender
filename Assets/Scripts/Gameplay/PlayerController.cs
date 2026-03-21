using UnityEngine.InputSystem;
using UnityEngine;
using Core;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation
namespace Gameplay {
    [System.Serializable]
    public struct PlayerForm {
        public Sprite turretSprite;
        public GameObject bulletPrefab;
        public float flex;
    }
    
    [DefaultExecutionOrder(-100)]                                               // Initiate first
    public class PlayerController : MonoBehaviour {
        [Header("Shoot settings")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform firePoint;
        [SerializeField, Range(0, 180)] private float flexibility = 15;

        [Header("Forms Configuration")]
        [SerializeField] private PlayerForm normalForm;
        [SerializeField] private PlayerForm superForm;

        [Header("References")]
        [SerializeField] private SpriteRenderer turretRenderer;

        private Player _logic;
        private Camera _camera;
        private bool _isSuperForm;

        public Player Player => _logic;

        public static PlayerController Instance { get; private set; }

        private void Awake() {
            if (Instance && Instance != this) { Destroy(gameObject); return; }
            Instance = this;

            _logic = new Player();
        }

        private void Start() {
            _camera = Camera.main;
            if (!firePoint) firePoint = transform.GetChild(0);
        }

        void Update() {
            CheckFormTransformation();
            HandleRotation();
            HandleShoot();
        }

        private void CheckFormTransformation() {
            float currentMult = ScoreController.Instance.GetMultiplier();

            if (!_isSuperForm && currentMult >= ScoreController.Instance.GetMaxMult() - 1) {
                _isSuperForm = true;
                ApplyForm(superForm);
                AudioManager.Instance.PlaySuperForm();
            } else if (_isSuperForm && currentMult <= ScoreController.Instance.GetMinMult() + 1) {
                _isSuperForm = false;
                ApplyForm(normalForm);
            }
        }
        
        private void ApplyForm(PlayerForm form) {
            turretRenderer.sprite = form.turretSprite;
            bulletPrefab = form.bulletPrefab;
            flexibility = form.flex;
        }

        private void HandleRotation() {
            if (!_camera) return;

            Vector2 mousePos = Mouse.current.position.ReadValue();
            if (IsMouseOutsideWindow(mousePos)) return;

            Vector3 worldMousePos = _camera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        
            RotateTurret(worldMousePos);
        }

        private bool IsMouseOutsideWindow(Vector2 mousePos) {
            return mousePos.x < 0 || mousePos.y < 0 || mousePos.x > Screen.width || mousePos.y > Screen.height;
        }

        private void RotateTurret(Vector3 target) {
            if (Time.timeScale <= 0) return;

            Vector2 direction = target - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (angle >= flexibility && angle <= 180 - flexibility) 
                transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }

        private void HandleShoot() {
            if (Pointer.current != null && Mouse.current.leftButton.wasPressedThisFrame && Time.timeScale > 0) Shoot();
        }

        private void Shoot() {
            if (!bulletPrefab || !firePoint) return;
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            if (_isSuperForm) AudioManager.Instance.PlayRandomBigShoot();
            else AudioManager.Instance.PlayRandomShoot();
        }

        public void OnHit(int dmg) {
            _logic.TakeDamage(dmg);
            GetComponent<HitFlash>()?.Flash();
        }
        
        private void OnDestroy() {
            if (Instance == this) Instance = null;
        }
    }
}
