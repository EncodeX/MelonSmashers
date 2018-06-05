using UnityEngine;

namespace Code.Danmaku.SceneSettings {
    public class Stage1Scene1 : SceneSetting {
	    private BulletPatternBuilder _patternManager;

        public void AddActions(Scene scene) {
	        _patternManager = BulletPatternBuilder.GetInstance();
                
	        
	        // build a action queue for shooter using AddAction()
	        scene.AddAction(
		        SceneActionBuilder.NewAction()
			        .SetEnterPosition(new Vector2(0, 13)).SetEnemyColor("cyan")  // "cyan" "yellow" or "magenta", default to "magenta" if unspecified
			        .SetAngle(-90).SetSpeed(16)						// this shooter will use a 16 speed at first 
			        .SetActionTime(120).SetSpeedOffset(-8)			// then stops when its speed equals 0 (2 sec).
			        .AddPattern(_patternManager.GetPattern("never_shoot"))
			        .AddAction()									// as soon as the speed goes to 0, its action
			        .SetSpeed(0).SetSpeedOffset(0)					// changes to this one, which stay still in its
			        .AddPattern(_patternManager.GetPattern("never_shoot")) // position.
			        .Build()										// you can also make it moving slowly by adding
	        );														// a small speed offset value
	        
	        
			scene.AddAction(
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(-7, 13))
				.SetAngle(-90)
				.SetSpeed(7)
				.AddPattern(_patternManager.GetPattern("p001"))
				.Build()
			);
            scene.AddAction(
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(-3, 13))
                    .SetAngle(-90)
                    .SetSpeed(7)
                    .AddPattern(_patternManager.GetPattern("p001"))
                    .Build()
            );
			scene.AddAction(
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(3, 13))
				.SetAngle(-90)
				.SetSpeed(7)
				.AddPattern(_patternManager.GetPattern("p001"))
				.Build()
			);
			scene.AddAction(
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(7, 13))
				.SetAngle(-90)
				.SetSpeed(7)
				.AddPattern(_patternManager.GetPattern("p001"))
				.Build()
			);
            SceneActionBuilder.AddSequence(
                scene,
                SceneActionBuilder.NewAction()
					.SetEnterPosition(new Vector2(7, 13)).SetEnemyColor("yellow")
                    .SetAngle(-90)
                    .SetAngleOffset(-40)
                    .SetSpeed(10)
                    .AddPattern(_patternManager.GetPattern("p002"))
                    .SetDelay(2 * 60)
                    .Build(),
                45,
                10
            );
			SceneActionBuilder.AddSequence(
				scene,
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(-7, 13)).SetEnemyColor("yellow")
				.SetAngle(-90)
				.SetAngleOffset(40)
				.SetSpeed(10)
				.AddPattern(_patternManager.GetPattern("p002"))
				.SetDelay(2 * 60)
				.Build(),
				45,
				10
			);
            scene.AddAction(
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(0, 13))
                    .SetAngle(-90)
                    .SetSpeed(6)
                    .SetSpeedOffset(-2)
                    .AddPattern(_patternManager.GetPattern("p007"))
                    .SetDelay(720)
                    .Build()
            );
            scene.AddAction(
                SceneActionBuilder.NewAction()
					.SetEnterPosition(new Vector2(-5, 13)).SetEnemyColor("cyan")
                    .SetAngle(-90)
                    .SetSpeed(8)
                    .SetSpeedOffset(-2)
                    .AddPattern(_patternManager.GetPattern("p006"))
                    .SetDelay(960)
                    .Build()
            );
            scene.AddAction(
                SceneActionBuilder.NewAction()
					.SetEnterPosition(new Vector2(5, 13)).SetEnemyColor("cyan")
                    .SetAngle(-90)
                    .SetSpeed(8)
                    .SetSpeedOffset(-2)
                    .AddPattern(_patternManager.GetPattern("p006"))
                    .SetDelay(960)
                    .Build()
            );
			SceneActionBuilder.AddSequence(
				scene,
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(-7, 13))
				.SetAngle(-90)
				.SetAngleOffset(40)
				.SetSpeed(10)
				.AddPattern(_patternManager.GetPattern("p007_partial_destroyable"))
				.SetDelay(1400)
				.Build(),
				45,
				10
			);
			SceneActionBuilder.AddSequence(
				scene,
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(7, 13))
				.SetAngle(-90)
				.SetAngleOffset(-40)
				.SetSpeed(10)
				.AddPattern(_patternManager.GetPattern("p007_partial_destroyable"))
				.SetDelay(1400)
				.Build(),
				45,
				10
			);
			scene.AddAction(
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(-7, 13))
				.SetAngle(-90)
				.SetSpeed(8)
				.SetSpeedOffset(-2)
				.AddPattern(_patternManager.GetPattern("p003")).SetEnemyColor("yellow")
				.SetDelay(2050)
				.Build()
			);
			scene.AddAction(
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(7, 13))
				.SetAngle(-90)
				.SetSpeed(8)
				.SetSpeedOffset(-2)
				.AddPattern(_patternManager.GetPattern("p003"))
				.SetDelay(2050)
				.Build()
			);
			scene.AddAction(
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(-5, 13)).SetEnemyColor("cyan")
				.SetAngle(-90)
				.SetSpeed(8)
				.SetSpeedOffset(-2)
				.AddPattern(_patternManager.GetPattern("p004"))
				.SetDelay(2400)
				.Build()
			);
			scene.AddAction(
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(5, 13)).SetEnemyColor("cyan")
				.SetAngle(-90)
				.SetSpeed(8)
				.SetSpeedOffset(-2)
				.AddPattern(_patternManager.GetPattern("p004"))
				.SetDelay(2400)
				.Build()
			);
			scene.AddAction(
				SceneActionBuilder.NewAction()
				.SetEnterPosition(new Vector2(0, 13)).SetEnemyColor("yellow")
				.SetAngle(-90)
				.SetSpeed(8)
				.SetSpeedOffset(-2)
				.AddPattern(_patternManager.GetPattern("p005"))
				.SetDelay(2800)
				.Build()
			);

	        // this one should be the miniboss
	        scene.AddAction(
		        SceneActionBuilder.NewAction()
			        .SetEnterPosition(new Vector2(0, 13)).SetEnemyColor("cyan").SetHealth(36000)
			        .SetAngle(-90).SetSpeed(16).SetSpeedOffset(-8).SetPrefabName("Boss")
			        .SetActionTime(120)//.AddPattern(_patternManager.GetPattern("never_shoot"))
			        .SetDelay(50 * 60)
			        .AddChild(
				        SceneActionBuilder.NewAction()
					        .SetEnterPosition(new Vector2(2.5f, 13)).SetEnemyColor("yellow").SetHealth(3000)
					        .SetAngle(-90).SetSpeed(16).SetSpeedOffset(-8).SetPrefabName("BossWing")
					        .SetActionTime(120).SetDelay(50 * 60).SetDropItem(DropItemType.Health, 100)
					        .AddAction().AddPattern(_patternManager.GetPattern("p000")).SetSpeed(3)
					        .SetAngleOffset(30).SetAngle(90)
					        .Build())
			        .AddChild(
				        SceneActionBuilder.NewAction()
					        .SetEnterPosition(new Vector2(-2.5f, 13)).SetEnemyColor("magenta").SetHealth(3000)
					        .SetAngle(-90).SetSpeed(16).SetSpeedOffset(-8).SetPrefabName("BossWing")
					        .SetActionTime(90).SetDelay(50 * 60).SetDropItem(DropItemType.Health, 100)
					        .AddAction().AddPattern(_patternManager.GetPattern("p000")).SetSpeed(3)
					        .SetAngleOffset(30).SetAngle(-90)
					        .Build())
			        .AddAction().SetHealthThreshold(0.7f).SetSpeed(0)
			        .AddPattern(_patternManager.GetPattern("8_way_blocking"))
			        .AddAction().SetHealthThreshold(0.3f).SetSpeed(0)
			        .AddPattern(_patternManager.GetPattern("4_way_spiral"))
			        .AddAction().SetSpeed(0)
			        .AddPattern(_patternManager.GetPattern("4_way_spiral_acc"))
			        .AddPattern(_patternManager.GetPattern("p000"))
			        .Build()
	        );
        }
    }
}