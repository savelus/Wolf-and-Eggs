using MiniGames.WolfAndEggs.Eggs;
using MiniGames.WolfAndEggs.Hearts;
using MiniGames.WolfAndEggs.Roosts;
using MiniGames.WolfAndEggs.Score;
using UnityEngine.Serialization;
using Zenject;

namespace MiniGames.WolfAndEggs.DI.Installers
{
    public class MainInstaller : MonoInstaller
    {
        public HeartView heartView;
        public ScoreView scoreView;
        public RoostSetup roostSetup;
        public SceneSettings sceneSettings;
        
        public override void InstallBindings()
        {
            Container
                .BindInstance(heartView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<HeartController>()
                .AsSingle();

            Container
                .BindInstance(scoreView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ScoreController>()
                .AsSingle();

            Container
                .BindInstance(roostSetup)
                .AsSingle();
            
           Container
                .BindInterfacesAndSelfTo<GameController>()
                .AsSingle();

            Container
                .BindInstance(sceneSettings)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<EggController>()
                .AsSingle();
        }
    }
}