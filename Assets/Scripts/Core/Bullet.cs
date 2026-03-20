using Gameplay;

namespace Core {
    public class Bullet {
        private readonly int _damage;

        public Bullet(int damage=1) {
            _damage = damage;
        }

        public void TouchEnemy(EnemyController enemy) {
            enemy.OnHit(_damage);
        }
    }
}
