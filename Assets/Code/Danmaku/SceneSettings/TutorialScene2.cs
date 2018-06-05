using UnityEngine;
using UnityEngine.UI;

namespace Code.Danmaku.SceneSettings {
	public class TutorialScene2 : SceneSetting {
		private BulletPatternBuilder _patternManager;

		public void AddActions(Scene scene) {
			_patternManager = BulletPatternBuilder.GetInstance();

			SceneActionBuilder.AddSequence(
				scene,
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(-10, 4)).SetEnemyColor("yellow")
				.SetDropItem(DropItemType.None)
				.SetAngle(0).SetSpeed(10)
				.AddPattern(_patternManager.GetPattern("never_shoot"))
				.Build(),
				25,
				12
			);
			SceneActionBuilder.AddSequence(
				scene,
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(6, 13)).SetEnemyColor("magenta")
				.SetDropItem(DropItemType.None)
				.SetAngle(-90).SetSpeed(10)
				.AddPattern(_patternManager.GetPattern("aim_once_after_2s"))
				.SetDelay(5 * 60).Build(),
				25,
				12
			);
			SceneActionBuilder.AddSequence(
				scene,
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(-6, 13)).SetEnemyColor("cyan")
				.SetDropItem(DropItemType.None)
				.SetAngle(-90).SetSpeed(10)
				.AddPattern(_patternManager.GetPattern("p001"))
				.SetDelay(10 * 60).Build(),
				25,
				12
			);
		}
	}
}