using NUnit.Framework;
using Core;

namespace Tests.EditMode {
    [TestFixture]
    public class EnemyTests {
        private Enemy _enemy;

        [SetUp]
        public void SetUp() {
            _enemy = new Enemy(); 												// Init before test
        }

        [Test]
        public void Enn_TakeDamage_Normal_ReducesHealth() {
            _enemy.TakeDamage(20);
            Assert.AreEqual(30, _enemy.Health);
        }

		[Test]
		public void Enn_TakeDamage_Limite_Death() {
			_enemy.TakeDamage(_enemy.MaxHealth);
			Assert.AreEqual(0, _enemy.Health);
		}

		[Test]
		public void Enn_TakeDamage_Limite_Overkill() {
			_enemy.TakeDamage(2500);
			Assert.AreEqual(0, _enemy.Health);
		}

		[Test]
		public void Enn_IsAlive_Normal_False() {
			_enemy.TakeDamage(_enemy.MaxHealth + 1);
			Assert.AreEqual(false, _enemy.IsAlive);
		}

		[Test]
		public void Enn_GetReward_Normal_correctPointValue() {
			_enemy.TakeDamage(_enemy.MaxHealth + 1);							// Kill enemy to allow to get reward
			int value = _enemy.GetReward();
			Assert.AreEqual(10, value);
		}

		[Test]
		public void Enn_GetReward_Limite_Zero() {
			_enemy.TakeDamage(_enemy.MaxHealth + 1);
			_enemy.GetReward();													// Lose of reward (simulate loot)
			int value = _enemy.GetReward();
			Assert.AreEqual(0, value);
		}

		[Test]
		public void Enn_TakeDamage_Limite_HealthZero() {
			_enemy.TakeDamage(_enemy.MaxHealth + 1);
			Assert.AreEqual(false, _enemy.IsAlive);
			_enemy.TakeDamage(_enemy.MaxHealth + 1);							// Damage already dead enemy
			Assert.AreEqual(0, _enemy.Health);
		}
    }
}
