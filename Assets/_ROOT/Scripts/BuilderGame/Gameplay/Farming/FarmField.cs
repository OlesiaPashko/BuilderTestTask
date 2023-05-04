namespace BuilderGame.Gameplay.Farming
{
    public interface IFarmField
    {
        FarmFieldState State { get; }

        public void SetPlanting();
    }
    
    public class FarmField : IFarmField
    {
        public FarmFieldState State { get; private set; }

        public void SetPlanting()
        {
            State = FarmFieldState.Planting;
        }
    }

    public enum FarmFieldState
    {
        RemovingGrass,
        Planting
    }
}