using UnityEngine;
using UnityEngine.UI;

namespace Code.Danmaku.SceneSettings {
	public class TutorialScene3 : SceneSetting {
		private BulletPatternBuilder _patternManager;

		public void AddActions(Scene scene) {
			_patternManager = BulletPatternBuilder.GetInstance();

			scene.AddAction (
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(-3, 13)).SetEnemyColor("yellow")
				.SetDropItem(DropItemType.Powerup, 100).SetAngle(-90).SetSpeed(16)
				.SetActionTime(120).SetSpeedOffset(-8)
				.AddPattern(_patternManager.GetPattern("never_shoot"))
				.AddAction()
				.SetSpeed(0).SetSpeedOffset(0)
				.AddPattern(_patternManager.GetPattern("never_shoot"))
				.Build()
			);
			scene.AddAction (
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(3, 13)).SetEnemyColor("cyan")
				.SetDropItem(DropItemType.Health, 100).SetAngle(-90).SetSpeed(16)
				.SetActionTime(120).SetSpeedOffset(-8)
				.AddPattern(_patternManager.GetPattern("never_shoot"))
				.AddAction()
				.SetSpeed(0).SetSpeedOffset(0)
				.AddPattern(_patternManager.GetPattern("never_shoot"))
				.Build()
			);
		}
	}
}