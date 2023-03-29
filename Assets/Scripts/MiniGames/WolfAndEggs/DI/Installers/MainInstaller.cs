using MiniGames.WolfAndEggs.Eggs;
using MiniGames.WolfAndEggs.Hearts;
using MiniGames.WolfAndEggs.Roosts;
using MiniGames.WolfAndEggs.Score;
using Zenject;

namespace MiniGames.WolfAndEggs.DI.Installers
{
    public class MainInstaller : MonoInstaller
    {
        public HeartView HeartView;
        public ScoreView ScoreView;
        public RoostSetup RoostSetup;
        public GameController GameController;
        public EggController EggController;
        public override void InstallBindings()
        {
            Container
                .BindInstance(HeartView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<HeartController>()
                .AsSingle();

            Container
                .BindInstance(ScoreView)
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ScoreController>()
                .AsSingle();

            Container
                .BindInstance(RoostSetup)
                .AsSingle();
            
            Container
                .BindInstance(EggController)
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInstance(GameController)
                .AsSingle();

            
        }
    }
}