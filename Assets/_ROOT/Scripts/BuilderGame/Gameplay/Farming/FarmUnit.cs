namespace BuilderGame.Gameplay.Farming
{
    using System;
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
        }

        private void SetStateToGround()
        {
            State = FarmUnitState.Ground;
            OnStateUpdated?.Invoke();
            isTriggered = false;
        }
        
        private void Plant()
        {
            OnStateUpdated?.Invoke();
            State = FarmUnitState.PlantGrowing;
        }

        private void OnDestroy()
        {
            grassUnit.OnDestroyed -= SetStateToGround;
        }
    }

    public enum FarmUnitState
    {
        Grass,
        Ground,
        PlantGrowing
    }
}