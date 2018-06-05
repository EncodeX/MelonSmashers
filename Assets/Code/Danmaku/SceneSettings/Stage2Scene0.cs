using UnityEngine;

namespace Code.Danmaku.SceneSettings {
    public class Stage2Scene0 : SceneSetting {
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
//            SceneActionBuilder.AddSequence(
//                scene,
//                SceneActionBuilder.NewAction().Build(),
//                15);
            
            // Scene Design
            
            SceneActionBuilder.AddSequence(
                scene,
                SceneActionBuilder.NewAction()
                    .SetRandomEnterPosition(new Vector2(-8, 8), new Vector2(13, 13)).SetEnemyColor("cyan")
                    .SetSpeed(8).SetSpeedOffset(-1)
                    .AddPattern("aim_once_after_1s").SetShootProbability(0.2f)
                    .Build(),
                number: 20,
                intervalRange: new Vector2(15, 30));
            
            SceneActionBuilder.AddSequence(
                scene,
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(-9, -1)).SetAngle(75).SetAngleOffset(-20).SetSpeed(6)
                    .SetEnemyColor("magenta").AddPattern("1_way_aiming_randomized_CD")
                    .SetDelay(8 * 60).Build(),
                40, 15);
            
            SceneActionBuilder.AddSequence(
                scene,
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(9, 0)).SetAngle(105).SetAngleOffset(20).SetSpeed(6)
                    .SetEnemyColor("magenta").AddPattern("1_way_aiming_randomized_CD")
                    .SetDelay(10 * 60).Build(),
                40, 15);
            
            scene.AddAction(
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(-9, 13)).SetAngle(-45).SetSpeed(8).SetSpeedOffset(-1)
                    .SetEnemyColor("yellow").AddPattern("2_way_spiral_CD").AddPattern("2_way_spiral_CD_0deg")
                    .SetDelay(20 * 60)
                    .Build());
            
            scene.AddAction(
                SceneActionBuilder.NewAction()
                    .SetEnterPosition(new Vector2(9, 13)).SetAngle(-135).SetSpeed(8).SetSpeedOffset(-1)
                    .SetEnemyColor("cyan").AddPattern("2_way_spiral_CD").AddPattern("2_way_spiral_CD_0deg")
                    .SetDelay(20 * 60)
                    .Build());
            
            SceneActionBuilder.AddSequence(
                scene,
                SceneActionBuilder.NewAction()
                    .SetRandomEnterPosition(new Vector2(-8, 8), new Vector2(13, 13)).SetRandomEnemyColor()
                    .SetSpeed(9).SetSpeedOffset(-.5f)
                    .AddPattern("aim_once_after_1s").SetShootProbability(0f)
                    .SetDelay(26 * 60)
                    .Build(),
                number: 30,
                intervalRange: new Vector2(15, 30));
        }
    }
}