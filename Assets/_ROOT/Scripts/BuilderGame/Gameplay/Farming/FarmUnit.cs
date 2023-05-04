namespace BuilderGame.Gameplay.Farming
{
    using System;
    using System.Threading.Tasks;
    using UnityEngine;
    using Zenject;

    public class FarmUnit : MonoBehaviour
    {
        [Inject]
        public IFarmField FarmField { get; set; }
        public float Size => 1f;

        public FarmUnitState State { get; private set; }

        public event Action OnStateUpdated;
        
        [SerializeField] private GrassUnit grassUnit;
        [SerializeField] private Plant plant;

        private bool isTriggered;

        private void Start()
        {
            grassUnit.OnDestroyed += SetStateToGround;
            plant.OnPlantReady += SetPlantReady;
            plant.OnPlantRaised += SetPlantRaised;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (isTriggered)
            {
                return;
            }

            if (State == FarmUnitState.Grass)
            {
                isTriggered = true;
                grassUnit.DestroyWithAnimation();
            }
            else if(FarmField.State == FarmFieldState.Planting && State == FarmUnitState.Ground)
            {            
                isTriggered = true;
                plant.StartGrowing();
            }
            else if(FarmField.State == FarmFieldState.Planting && State == FarmUnitState.PlantReady)
            {            
                isTriggered = true;
                plant.Raise();
            }
        }

        private void SetStateToGround()
        {
            UpdateState(FarmUnitState.Ground);
        }
        
        private void SetPlantReady()
        {
            UpdateState(FarmUnitState.PlantReady);
        }
        private void SetPlantRaised()
        {
            
            UpdateState(FarmUnitState.Ground);
        }

        private void UpdateState(FarmUnitState state)
        {
            State = state;
            OnStateUpdated?.Invoke();
            isTriggered = false;
        }

        private void OnDestroy()
        {
            grassUnit.OnDestroyed -= SetStateToGround;
            plant.OnPlantReady -= SetPlantReady;
            plant.OnPlantRaised -= SetPlantRaised;
        }
    }

    public enum FarmUnitState
    {
        Grass,
        Ground,
        PlantReady
    }
}