using UnityEngine;
using Core;

namespace Gameplay {
    public class BulletController : MonoBehaviour {
        [SerializeField] private bool hasPenetration;
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 5f;
        [SerializeField] private int damage = 1;
        [SerializeField, Range(0, 100)] private int cost = 5;                   // Energy cost

        private Bullet _logic;
        public int Cost => cost;

        void Start() {
            _logic = new Bullet(damage);
            Destroy(gameObject, lifeTime);                                      // Auto-delete self after a few seconds
        }

        void Update() {
            Move();
        }

        private void Move() {
            transform.Translate(Vector3.up * (speed * Time.deltaTime));
        }

        private void OnTriggerEnter2D(Collider2D other) {
            EnemyController enemy = other.GetComponent<EnemyController>();

            if (enemy) {
                _logic.TouchEnemy(enemy);
                if (!hasPenetration) Destroy(gameObject);
            }
        }
    }
}
