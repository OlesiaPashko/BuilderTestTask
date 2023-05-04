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

        private void Start()
        {
            grassUnit.OnDestroyed += SetStateToGround;
        }

        private void SetStateToGround()
        {
            OnStateUpdated?.Invoke();
            State = FarmUnitState.Ground;
        }

        private void OnDestroy()
        {
            grassUnit.OnDestroyed -= SetStateToGround;
        }
    }

    public enum FarmUnitState
    {
        Grass,
        Ground
    }
}