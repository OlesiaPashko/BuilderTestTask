namespace BuilderGame.Gameplay.Farming
{
    using System;
    using UnityEngine;

    public class FarmUnit : MonoBehaviour
    {
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
            isTriggered = true;
            if (State == FarmUnitState.Grass)
            {
                grassUnit.DestroyWithAnimation();
            }
            else if(State == FarmUnitState.Ground)
            {
                plant.StartGrowing();
            }
        }

        private void SetStateToGround()
        {
            OnStateUpdated?.Invoke();
            State = FarmUnitState.Ground;
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