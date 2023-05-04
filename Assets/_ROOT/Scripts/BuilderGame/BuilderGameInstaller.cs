using Zenject;

namespace BuilderGame
{
    using Gameplay.Farming;

    public class BuilderGameInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BuilderGameInstaller>().FromInstance(this);
            Container.BindInterfacesAndSelfTo<FarmField>().AsSingle();
        }

        public void Initialize()
        {
            
        }
    }
}