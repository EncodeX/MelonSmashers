using UnityEngine;
using UnityEngine.UI;

namespace Code.Danmaku.SceneSettings {
	public class TutorialScene4 : SceneSetting {
		private BulletPatternBuilder _patternManager;

		public void AddActions(Scene scene) {
			_patternManager = BulletPatternBuilder.GetInstance();

			SceneActionBuilder.AddSequence(
				scene,
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(10, 4)).SetEnemyColor("yellow")
				.SetDropItem(DropItemType.None)
				.SetAngle(180).SetSpeed(10)
				.AddPattern(_patternManager.GetPattern("p001"))
				.Build(),
				25,
				12
			);
		}
	}
}