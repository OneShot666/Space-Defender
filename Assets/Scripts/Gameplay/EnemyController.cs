using UnityEngine;
using Core;

namespace  Gameplay {
    public class EnemyController : MonoBehaviour {
        [Header("Settings")]
        [SerializeField] private bool isRandom = true;
        [SerializeField] private EnemyType type = EnemyType.Basic;

        [Header("Visuals")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite basicSprite;
        [SerializeField] private Sprite fastSprite;
        [SerializeField] private Sprite tankSprite;

        private Enemy _logic;

        private void Start() {
            if (isRandom) {                                                         // Choose random enemy type
                System.Array values = System.Enum.GetValues(typeof(EnemyType));
                type = (EnemyType)values.GetValue(Random.Range(0, values.Length));
            }
            _logic = new Enemy(type);
            
            ApplyVisual();
        }

        void Update() {
            Move();

            CheckAttack();
        }

        private void ApplyVisual() {
            if (!spriteRenderer) spriteRenderer = GetComponent<SpriteRenderer>();

            switch (type) {
                case EnemyType.Basic:
                    spriteRenderer.sprite = basicSprite;
                    break;
                case EnemyType.Fast:
                    spriteRenderer.sprite = fastSprite;
                    transform.localScale = Vector3.one * 0.8f;
                    break;
                case EnemyType.Tank:
                    spriteRenderer.sprite = tankSprite;
                    transform.localScale = Vector3.one * 1.5f;
                    break;
            }
        }

        private void Move() {
            transform.Translate(Vector3.down * _logic.Speed * Time.deltaTime);
        }

        private void CheckAttack() {
            if (transform.position.y < -7f) {
                GameController.Instance.LoseLife();
                Destroy(gameObject);                                                // Auto-destroy if out of screen
            }
        }

        public void OnHit(int damage) {
            _logic.TakeDamage(damage);

            if (!_logic.IsAlive) {
                ScoreController.Instance.AddKill(_logic.GetReward());
                Destroy(gameObject);
            }
        }
    }
}
