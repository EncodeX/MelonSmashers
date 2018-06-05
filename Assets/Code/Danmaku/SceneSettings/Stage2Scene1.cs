using UnityEngine;

namespace Code.Danmaku.SceneSettings {
    public class Stage2Scene1 : SceneSetting {
        private BulletPatternBuilder _patternManager;

        public void AddActions(Scene scene) {
            _patternManager = BulletPatternBuilder.GetInstance();
            // build a action queue for shooter using AddAction()
//            scene.AddAction(
//                SceneActionBuilder.NewAction()
//                    .SetEnterPosition(new Vector2(0, 13)).SetEnemyColor("magenta") // "cyan" "yellow" or "magenta"
//                    .SetDropItem(DropItemType.None).SetAngle(-90).SetSpeed(16)                     // this shooter will use a 16 speed at first 
//                    .SetActionTime(120).SetSpeedOffset(-8) // then stops when its speed equals 0 (2 sec).
//                    .AddPattern(_patternManager.GetPattern("never_shoot"))
//                    .AddAction() // as soon as the speed goes to 0, its action
//                    .SetSpeed(0).SetSpeedOffset(0) // changes to this one, which stay still in its
//                    .AddPattern(_patternManager.GetPattern("never_shoot")) // position.
//                    .Build() // you can also make it moving slowly by adding
//            ); // a small speed offset value
            
            // template remember to comment it after use
            
//            scene.AddAction(
//                SceneActionBuilder.NewAction()
//                    .Build());
//            
            SceneActionBuilder.AddSequence(
                scene,
                SceneActionBuilder.NewAction().Build(),
                15);
            
            // Scene Design
            
            SceneActionBuilder.AddSequence(
                scene,
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(-9, 13)).SetAngle(-45).SetSpeed(8).SetSpeedOffset(-1)
                    .SetRandomEnemyColor().AddPattern("chop_-30deg_down")
                    .Build(),
                480, 6);
            
            SceneActionBuilder.AddSequence(
                scene,
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(9, 13)).SetAngle(-135).SetSpeed(8).SetSpeedOffset(-1)
                    .SetRandomEnemyColor().AddPattern("chop_-150deg_down")
                    .Build(),
                480, 6);
            
            SceneActionBuilder.AddSequence(
                scene,
                SceneActionBuilder.NewAction()
                    .SetRandomEnterPosition(new Vector2(-8, 8), new Vector2(13, 13)).SetEnemyColor("cyan")
                    .SetSpeed(8).SetSpeedOffset(-1)
                    .AddPattern("aim_once_after_1s").SetShootProbability(0.2f)
//                    .SetDelay(3 * 60)
                    .Build(),
                number: 20,
                intervalRange: new Vector2(45, 75));
            
            SceneActionBuilder.AddSequence(
                scene,
                SceneActionBuilder.NewAction()
                    .SetRandomEnterPosition(new Vector2(-8, 8), new Vector2(13, 13)).SetEnemyColor("yellow")
                    .SetSpeed(8).SetSpeedOffset(-.5f)
                    .AddPattern("3_way_acc").SetShootProbability(0.2f)
                    .SetDelay(9 * 60)
                    .Build(),
                number: 15,
                intervalRange: new Vector2(150, 180));
            
            scene.AddAction(
                SceneActionBuilder.NewAction().SetHealth(3000)
                    .SetEnterPosition(new Vector2(5, 13)).SetAngle(-90).SetSpeed(10).SetSpeedOffset(-7.5f)
                    .SetEnemyColor("yellow").SetDelay(30 * 60).SetActionTime(90)
                    .AddAction().SetSpeed(0).SetSpeedOffset(0).AddPattern("ring")
                    .Build());
            
            scene.AddAction(
                SceneActionBuilder.NewAction().SetHealth(3000)
                    .SetEnterPosition(new Vector2(-5, 13)).SetAngle(-90).SetSpeed(10).SetSpeedOffset(-7.5f)
                    .SetEnemyColor("magenta").SetDelay(42 * 60).SetActionTime(90)
                    .AddAction().SetSpeed(0).SetSpeedOffset(0).AddPattern("ring")
                    .Build());
//            
            SceneActionBuilder.AddSequence(
                scene,
                SceneActionBuilder.NewAction()
                    .SetRandomEnterPosition(new Vector2(-9, -9), new Vector2(1, 8))
                    .SetAngle(15).SetSpeed(6)
                    .SetRandomEnemyColor().AddPattern("1_way_aiming_randomized_CD")
                    .SetDelay(49 * 60).Build(),
                number: 30, intervalRange: new Vector2(45, 75));
//            
//            SceneActionBuilder.AddSequence(
//                scene,
//                SceneActionBuilder.NewAction()
//                    .SetRandomEnterPosition(new Vector2(9, 9), new Vector2(1, 8))
//                    .SetAngle(165).SetSpeed(6)
//                    .SetRandomEnemyColor().AddPattern("1_way_aiming_randomized_CD")
//                    .SetDelay(38 * 60 + 15).Build(),
//                number: 30, intervalRange: new Vector2(45, 75));
//            
//            SceneActionBuilder.AddSequence(
//                scene,
//                SceneActionBuilder.NewAction()
//                    .SetRandomEnterPosition(new Vector2(-8, 8), new Vector2(13, 13)).SetRandomEnemyColor()
//                    .SetSpeed(9).SetSpeedOffset(-.5f)
//                    .AddPattern("aim_once_after_1s").SetShootProbability(0f)
//                    .SetDelay(26 * 60)
//                    .Build(),
//                number: 30,
//                intervalRange: new Vector2(15, 30));
        }
    }
}