namespace BuilderGame.Gameplay.Unit
{
    using DG.Tweening;
    using UnityEngine;

    public class PumpingAnimator : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private Ease ease = Ease.InSine;

        private bool isTriggered;

        public void Pump()
        {
            if (isTriggered)
            {
                return;
            }
            isTriggered = true;
            DOTween.Sequence().Join(transform.DOScale(scale, duration)
                    .SetRelative(true)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetEase(ease))
                .AppendCallback(() => isTriggered = false);
        }
    }
}