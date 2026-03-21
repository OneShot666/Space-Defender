using UnityEngine.InputSystem;
using UnityEngine;
using Core;

// ReSharper disable Unity.PerformanceCriticalCodeInvocation
namespace Gameplay {
    [System.Serializable]
    public struct PlayerForm {
        public Sprite turretSprite;
        public BulletController bulletPrefab;
        [Range(0, 180)] public float flex;
        [Range(0, 100)] public float regen;
    }

    [DefaultExecutionOrder(-100)]                                               // Initiate first
    public class PlayerController : MonoBehaviour {
        [Header("Forms Configuration")]
        [SerializeField] private PlayerForm normalForm;
        [SerializeField] private PlayerForm superForm;

        [Header("References")]
        [SerializeField] private SpriteRenderer turretRenderer;
        [SerializeField] private Transform firePoint;

        private Player _logic;
        private Camera _camera;
        private BulletController _bulletPrefab;
        private bool _isSuperForm;
        private float _flexibility;
        private float _regen;

        public Player Player => _logic;
        public float Regen => _regen;

        public static PlayerController Instance { get; private set; }

        private void Awake() {
            if (Instance && Instance != this) { Destroy(gameObject); return; }
            Instance = this;

            _logic = new Player();
        }

        private void Start() {
            _camera = Camera.main;
            if (!firePoint) firePoint = transform.GetChild(0);
            ApplyForm(normalForm);
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
            _bulletPrefab = form.bulletPrefab;
            _flexibility = form.flex;
            _regen = form.regen;
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

            if (angle >= _flexibility && angle <= 180 - _flexibility) 
                transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }

        private void HandleShoot() {
            if (Pointer.current != null && Mouse.current.leftButton.wasPressedThisFrame && Time.timeScale > 0) Shoot();
        }

        private void Shoot() {
            if (!_bulletPrefab || !firePoint) return;
            if (!_logic.CanShoot(_bulletPrefab.Cost)) return;

            Instantiate(_bulletPrefab, firePoint.position, transform.rotation);
            _logic.Consume(_bulletPrefab.Cost);

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
