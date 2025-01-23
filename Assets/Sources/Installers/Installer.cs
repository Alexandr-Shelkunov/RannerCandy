using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace Alexander.RunnerCandy
{
    public class Installer : LifetimeScope
    {
        [SerializeField] private GameObject[] platformsPrefabs;
        [SerializeField] private GameObject losePanel; 
        [SerializeField] private GameObject playPanel;

        [SerializeField] private Transform playerTransform;
        [SerializeField] private Transform spawnRootT;

        [SerializeField] private Slider weightSlider;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip candyPickupSound;


        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<LevelGenerator>(Lifetime.Singleton).
                WithParameter("platformsPrefabs", platformsPrefabs).
                WithParameter("playerT", playerTransform).
                WithParameter("spawnRootT", spawnRootT).
                AsImplementedInterfaces();

            builder.Register<InitializeController>(Lifetime.Singleton).
                AsImplementedInterfaces();

            builder.Register<LoopController>(Lifetime.Singleton).
                AsImplementedInterfaces();

            builder.Register<WeightBar>(Lifetime.Singleton).
            WithParameter("weightSlider", weightSlider).
            WithParameter("losePanel", losePanel).
            WithParameter("playerTransform", playerTransform).
            AsImplementedInterfaces();

            builder.RegisterComponentInHierarchy<WeightBarManager>();

            builder.Register<PlayerModel>(Lifetime.Singleton);

            builder.Register<CandyPickupChecker>(Lifetime.Singleton).
               WithParameter("playerTransform", playerTransform).
               AsImplementedInterfaces();

            builder.Register<CandyPickupAudioPlayer>(Lifetime.Singleton).
               WithParameter("audioSource", audioSource).
               WithParameter("candyPickupSound", candyPickupSound).
               WithParameter("playerTransform", playerTransform).
               AsImplementedInterfaces();

            builder.Register<ObstacleCollisionChecker>(Lifetime.Singleton).
                WithParameter("playerTransform", playerTransform).
                WithParameter("losePanel", losePanel).
                WithParameter("playPanel", playPanel).
                AsImplementedInterfaces();

            builder.Register<PlayerMovement>(Lifetime.Singleton).
                WithParameter("playerT", playerTransform).
                WithParameter("lineDistance", 2.0F).
                WithParameter("jumpForce", 10.0F).
                WithParameter("fallForce", 1.0F).
                WithParameter("intialSpeed", 1.0F).
                WithParameter("verticalHeightThreshold", 0.01F).
                AsImplementedInterfaces();

            builder.Register<KeyboardInput>(Lifetime.Singleton).
                AsImplementedInterfaces();
        }
    }
}
