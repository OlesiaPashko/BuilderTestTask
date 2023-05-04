namespace BuilderGame.Gameplay.Farming
{
    using System;
    using DG.Tweening;
    using UnityEngine;

    public class GrassUnit : MonoBehaviour
    {
        [SerializeField] private GrassUnitAnimator[] animators;
        public event Action OnDestroyed;

        public void DestroyWithAnimation()
        {
            foreach (var animator in animators)
            {
                animator.Pump().AppendCallback(SelfDestroy);
            }
        }

        private void SelfDestroy()
        {
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}