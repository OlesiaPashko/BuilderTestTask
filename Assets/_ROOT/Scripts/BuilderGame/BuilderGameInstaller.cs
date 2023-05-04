using Zenject;

namespace BuilderGame
{
    using Gameplay.Farming;
    using Gameplay.Unit;
    using UnityEngine;

    public class BuilderGameInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private Player player;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BuilderGameInstaller>().FromInstance(this);
            Container.BindInterfacesAndSelfTo<FarmField>().AsSingle();
            Container.Bind<Player>().FromInstance(player).AsSingle();
        }

        public void Initialize()
        {
            
        }
    }
}