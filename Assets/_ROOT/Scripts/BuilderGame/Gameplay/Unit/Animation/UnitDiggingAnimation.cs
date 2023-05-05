namespace BuilderGame.Gameplay.Unit.Animation
{
    using System;
    using Equipment;
    using UnityEngine;

    public class UnitDiggingAnimation : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        
        [SerializeField]
        private EquipmentContainer equipmentContainer;
        
        [SerializeField]
        private float transitionSpeed = 0.1f;
        
        [SerializeField]
        private float animationTime = 1f;
        
        private float diggingAmount = 0f;
        private float diggingDuration = 0f;
        private DiggingState DiggingState;
        
        
        public void SetDigging()
        {
            if (DiggingState == DiggingState.NoDigging)
            {
                DiggingState = DiggingState.TransitionToDigging;
                equipmentContainer.AddShovel();
            }
            else if(DiggingState == DiggingState.Digging)
            {
                diggingDuration = 0f;
            }
        }

        private void OnValidate()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            if (DiggingState == DiggingState.NoDigging)
            {
                return;
            }
            
            if (DiggingState == DiggingState.TransitionToDigging)
            {
                TransitToDigging();
            }
            else if (DiggingState == DiggingState.TransitionFromDigging)
            {
                TransitFromDigging();
            }
            else if(DiggingState == DiggingState.Digging)
            {
                Dig();
            }
            animator.SetLayerWeight(1, diggingAmount);

        }

        private void Dig()
        {
            if (diggingDuration < animationTime)
            {
                diggingDuration += Time.deltaTime;
            }
            else
            {
                diggingDuration = 0f;
                DiggingState = DiggingState.TransitionFromDigging;
            }
        }

        private void TransitFromDigging()
        {
            if (diggingAmount > 0f)
            {
                diggingAmount -= transitionSpeed;
            }
            else
            {
                DiggingState = DiggingState.NoDigging;
                equipmentContainer.RemoveShovel();
            }
        }

        private void TransitToDigging()
        {
            if (diggingAmount < 0.6f)
            {
                diggingAmount += transitionSpeed;
            }
            else
            {
                DiggingState = DiggingState.Digging;
            }
        }
    }

    public enum DiggingState
    {
        NoDigging,
        TransitionToDigging,
        Digging,
        TransitionFromDigging
    }
}