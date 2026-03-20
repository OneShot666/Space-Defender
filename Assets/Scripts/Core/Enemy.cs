using UnityEngine;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Core {
    public enum EnemyType { Basic, Fast, Tank }

    public class Enemy {
        public int Health { get; private set; }
        public readonly int MaxHealth = 100;
        public int Speed { get; private set; }
        private int PointValue { get; set; }                                    // Given score when die
        public EnemyType Type { get; private set; }
        public bool IsAlive => Health > 0;
        private bool _looted;

        public Enemy(EnemyType type=EnemyType.Basic) {
            Type = type;
            switch (type) {
                case EnemyType.Basic:
                    Health = 3;
                    Speed = 2;
                    PointValue = 1;
                    break;
                case EnemyType.Fast:
                    Health = 2;
                    Speed = 4;
                    PointValue = 2;
                    break;
                case EnemyType.Tank:
                    Health = 6;
                    Speed = 1;
                    PointValue = 3;
                    break;
            }
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
