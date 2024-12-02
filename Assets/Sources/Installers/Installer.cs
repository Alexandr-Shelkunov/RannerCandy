using Alexander.RunnerCandy;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Alexander.RunnerCandy
{
    public class Installer : LifetimeScope
    {
        [SerializeField] private GameObject[] platformsPrefabs;
        [SerializeField] private Transform player;
        [SerializeField] private Transform spawnRoot;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LevelGenerator>(Lifetime.Singleton).
                WithParameter("platformsPrefabs", platformsPrefabs).
                WithParameter("player", player).
                WithParameter("spawnRoot", spawnRoot).
                AsImplementedInterfaces();

            builder.RegisterEntryPoint<LoopController>(Lifetime.Singleton);

            builder.Register<Test>(Lifetime.Singleton).
                AsImplementedInterfaces();
        }
    }
}
