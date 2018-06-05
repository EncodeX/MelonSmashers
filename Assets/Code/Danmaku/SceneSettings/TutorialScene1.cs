using UnityEngine;
using UnityEngine.UI;

namespace Code.Danmaku.SceneSettings {
	public class TutorialScene1 : SceneSetting {
		private BulletPatternBuilder _patternManager;

		public void AddActions(Scene scene) {
			_patternManager = BulletPatternBuilder.GetInstance();

			scene.AddAction (
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(-5, 13)).SetEnemyColor("cyan")
				.SetDropItem(DropItemType.None).SetAngle(-90)
				.SetSpeed(5)
				.AddPattern(_patternManager.GetPattern("p001"))
				.Build()
			);
			scene.AddAction (
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(0, 13)).SetEnemyColor("magenta")
				.SetDropItem(DropItemType.None).SetAngle(-90)
				.SetSpeed(5)
				.AddPattern(_patternManager.GetPattern("p001"))
				.Build()
			);
			scene.AddAction (
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(5, 13)).SetEnemyColor("yellow")
				.SetDropItem(DropItemType.None).SetAngle(-90)
				.SetSpeed(5)
				.AddPattern(_patternManager.GetPattern("p001"))
				.Build()
			);
		}
	}
}