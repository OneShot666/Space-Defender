using UnityEngine;

namespace Core {
    public class Player {
        public int Health { get; private set; } = 50;
        public readonly int MaxHealth = 100;
        public int Lives  { get; private set; } = 3;
        public int Score  { get; private set; }
        public bool IsAlive => Health > 0 && Lives > 0;

        public void TakeDamage(int amount) {
            if (IsAlive & amount > 0) Health -= Mathf.Min(amount, Health);
        }

        public void Heal(int amount) {
            if (IsAlive) Health += Mathf.Min(amount, MaxHealth - Health);
        }

        public void AddScore(int points) {
            if (points > 0) Score += points;
        }

        public void LoseLife() {
            Lives -= Mathf.Min(1, Lives);
        }
    }
}
