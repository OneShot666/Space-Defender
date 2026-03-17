using NUnit.Framework;
using Core;

namespace Tests.EditMode {
    [TestFixture]
    public class PlayerTests {
        private Player _player;

        [SetUp]
        public void SetUp() {
            _player = new Player(); 											// Init before test
        }

        [Test]
        public void TakeDamage_Normal_ReducesHealth() {
            _player.TakeDamage(20);
            Assert.AreEqual(30, _player.Health);
        }

		[Test]
		public void TakeDamage_Limite_ReducesHealth() {
			_player.TakeDamage(_player.MaxHealth + 1);
			Assert.AreEqual(0, _player.Health);
		}

		[Test]
		public void TakeDamage_Limite_UnchangedHealth() {
			_player.TakeDamage(0);
			Assert.AreEqual(50, _player.Health);
		}

		[Test]
		public void TakeDamage_Error_NegDamage() {
			_player.TakeDamage(-10);
			Assert.AreEqual(50, _player.Health);
		}

		[Test]
		public void Heal_Normal_IncreasesHealth() {
			_player.Heal(20);
			Assert.AreEqual(70, _player.Health);
		}

		[Test]
		public void Heal_Limite_IncreasesHealth() {
			_player.Heal(_player.MaxHealth + 1);
			Assert.AreEqual(_player.MaxHealth, _player.Health);
		}

		[Test]
		public void IsAlive_Normal_HealthAboveZero_True() {
			_player.TakeDamage(20);
			Assert.AreEqual(true, _player.IsAlive);
		}

		[Test]
		public void IsAlive_Limite_HealthEqualZero_False() {
			_player.TakeDamage(_player.MaxHealth + 1);
			Assert.AreEqual(false, _player.IsAlive);
		}

		[Test]
		public void LoseLife_Normal_ReducesLives() {
			_player.LoseLife();
            Assert.AreEqual(2, _player.Lives);
		}

		[Test]
		public void LoseLife_Limite_Death_False() {
			_player.LoseLife();
			_player.LoseLife();
			_player.LoseLife();
			_player.LoseLife();
            Assert.AreEqual(0, _player.Lives);
            Assert.AreEqual(false, _player.IsAlive);
		}

		[Test]
		public void AddScore_Normal_IncreasesScore() {
			_player.AddScore(10);
            Assert.AreEqual(10, _player.Score);
		}

		[Test]
		public void AddScore_Limite_UnchangedScore() {
			_player.AddScore(0);
            Assert.AreEqual(0, _player.Score);
		}

		[Test]
		public void AddScore_Error_UnchangedScore() {
			_player.AddScore(-10);
            Assert.AreEqual(0, _player.Score);
		}
    }
}
