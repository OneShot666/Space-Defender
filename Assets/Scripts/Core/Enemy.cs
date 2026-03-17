using UnityEngine;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Core {
    public enum EnemyType { Basic, Fast, Tank }

    public class Enemy {
        public int Health { get; private set; }
        public readonly int MaxHealth = 100;
        private int PointValue { get; set; }                                    // Given score when die
        public EnemyType Type { get; private set; }
        public bool IsAlive => Health > 0;
        private bool _looted;

        public Enemy(int health=50, int pointValue=10, EnemyType type=EnemyType.Basic) {
            Health = health;
            PointValue = pointValue;
            Type = type;
        }

        public void TakeDamage(int amount) {
            if (IsAlive) Health -= Mathf.Min(amount, Health);
        }

        public int GetReward() {
            if (!IsAlive & !_looted) { _looted = true; return PointValue; }
            return 0;
        }
    }
}
