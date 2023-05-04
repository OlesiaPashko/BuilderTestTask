namespace BuilderGame.Gameplay.Farming
{
    using System;
    using DG.Tweening;
    using UnityEngine;

    public class GrassUnit : MonoBehaviour
    {
        [SerializeField] private GrassUnitAnimator animator;
        
        public float Size => 1f;

        private void OnTriggerEnter(Collider other)
        {
            animator.Pump().AppendCallback(() => Destroy(gameObject));
        }

        private void OnValidate()
        {
            if (animator == null)
            {
                animator = GetComponent<GrassUnitAnimator>();
            }
        }
    }
}