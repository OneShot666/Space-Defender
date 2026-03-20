using UnityEngine;

namespace Core {
    public class ScoreCalculator {
        private int BaseScore => 10;
        public float Multiplier { get; private set; } = 1.0f;
        public readonly float MaxMult = 5.0f;

        public int Calculate(int score, int time=0) {
            return (int)(BaseScore * score * Multiplier);
        }

        public void ApplyCombo(int comboCount) {
            Multiplier += Mathf.Min(comboCount, MaxMult - Multiplier);
        }

        public void ResetMultiplier() {
            Multiplier = 1;
        }
    }
}
