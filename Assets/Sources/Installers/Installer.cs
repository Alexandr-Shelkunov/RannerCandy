using Alexander.RunnerCandy;
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

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LevelGenerator>(Lifetime.Singleton).
                WithParameter("platformsPrefabs", platformsPrefabs).
                WithParameter("playerT", playerT).
                WithParameter("spawnRootT", spawnRootT).
                AsImplementedInterfaces();

            builder.RegisterEntryPoint<LoopController>(Lifetime.Singleton);
            builder.RegisterEntryPoint<InitializeController>(Lifetime.Singleton);
            builder.Register<PlayerModel>(Lifetime.Singleton);

            builder.Register<Player>(Lifetime.Singleton)
                .WithParameter("playerT", playerT)
                .AsSelf();

            builder.Register<PlayerMovement>(Lifetime.Singleton).
                WithParameter("playerT", playerT).
                WithParameter("lineDistance", 2.0F).
                WithParameter("jumpForce", 10.0F).
                WithParameter("fallForce", 1.0F).
                WithParameter("intialSpeed", 1.0F).
                AsImplementedInterfaces();

            builder.Register<SwipeInput>(Lifetime.Singleton).
                AsImplementedInterfaces();

            builder.Register<KeyboardInput>(Lifetime.Singleton).
                AsImplementedInterfaces();
        }
    }
}
