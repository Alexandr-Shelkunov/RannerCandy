using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Alexander.RunnerCandy
{
    public class Installer : LifetimeScope
    {
        [SerializeField] private GameObject[] platformsPrefabs;
        [SerializeField] private Transform playerT;
        [SerializeField] private Transform spawnRootT;
        [SerializeField] private GameObject losePanel;
        [SerializeField] private Slider weightSlider;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LevelGenerator>(Lifetime.Singleton).
                WithParameter("platformsPrefabs", platformsPrefabs).
                WithParameter("playerT", playerT).
                WithParameter("spawnRootT", spawnRootT).
                AsImplementedInterfaces();

            builder.RegisterEntryPoint<InitializeController>(Lifetime.Singleton);
            builder.Register<PlayerModel>(Lifetime.Singleton);

            builder.Register<WeightBar>(Lifetime.Singleton);
                WithParameter("slider", weightSlider);

            builder.Register<PlayerMovement>(Lifetime.Singleton).
                WithParameter("playerT", playerT).
                WithParameter("lineDistance", 2.0F).
                WithParameter("jumpForce", 10.0F).
                WithParameter("fallForce", 1.0F).
                WithParameter("intialSpeed", 1.0F).
                WithParameter("losePanel", losePanel)
                AsImplementedInterfaces();

            builder.Register<Player>(Lifetime.Singleton)
                WithParameter("playerT", playerT)
                WithParameter("model", Container.Resolve<PlayerModel>()) 
                WithParameter("playerMovement", Container.Resolve<PlayerMovement>())
                WithParameter("weight", Container.Resolve<WeightBar>())
                AsSelf();

            //builder.Register<SwipeInput>(Lifetime.Singleton).
            //WithParameter("swipeRegisterOffset", 60).
            //AsImplementedInterfaces();

            builder.Register<KeyboardInput>(Lifetime.Singleton).
                AsImplementedInterfaces();
        }

        protected override void Awake()
        {
            var playerModel = Container.Resolve<PlayerModel>();
            var playerMovement = Container.Resolve<PlayerMovement>();
            var weightBar = Container.Resolve<WeightBar>();

            var player = Container.Resolve<Player>();
            player.Initialize(playerModel, playerMovement, weightBar);
        }
    }
}
