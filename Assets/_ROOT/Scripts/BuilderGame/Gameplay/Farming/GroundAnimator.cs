namespace BuilderGame.Gameplay.Farming
{
    using DG.Tweening;
    using UnityEngine;

    public class GroundAnimator : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);
        [SerializeField] private float duration = 0.2f;
        [SerializeField] private Ease ease = Ease.InSine;

        private bool isTriggered;

        private void OnTriggerEnter(Collider other)
        {
            if (isTriggered)
            {
                return;
            }

            Pump();
        }

        public void Pump()
        {
            isTriggered = true;
            DOTween.Sequence().Join(transform.DOScale(scale, duration)
                    .SetRelative(true)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetEase(ease))
                .AppendCallback(() => isTriggered = false);
        }
    }
}