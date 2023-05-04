namespace BuilderGame.Gameplay.Farming
{
    using System;
    using DG.Tweening;
    using UnityEngine;

    public class GrassUnit : MonoBehaviour
    {
        [SerializeField] private GrassUnitAnimator animator;
        public event Action OnDestroyed;
        
        private void OnTriggerEnter(Collider other)
        {
            animator.Pump().AppendCallback(SelfDestroy);
        }

        private void SelfDestroy()
        {
            OnDestroyed?.Invoke();
            Destroy(gameObject);
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