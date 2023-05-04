namespace BuilderGame.Gameplay.Farming
{
    using DG.Tweening;
    using UnityEngine;

    public class GrassUnitAnimator : MonoBehaviour
    {
        [Header("Settings")] 
        [Header("Punch")] 
        [SerializeField] private Ease punchEase;
        [SerializeField] private float punchDuration = 0.3f;
        [SerializeField] private Vector3 punch = new Vector3(0.1f,0.1f,0.1f);
        [SerializeField] private int vibrato = 2;
        [SerializeField, Range(0,1)] private float elasticity = 1f;
        
        [Header("Scale down")] 
        [SerializeField] private Ease scaleEase = Ease.InQuad;
        [SerializeField] private float scaleDuration = 0.5f;
        
        public Sequence Pump()
        {
            var sequence = DOTween.Sequence();
            var pump = transform.DOPunchScale(punch, punchDuration, vibrato, elasticity).SetEase(punchEase);
            var scale = transform.DOScale(new Vector3(0f, 0f, 0f), scaleDuration)
                .SetEase(scaleEase);
            sequence.Append(pump).Append(scale);
            return sequence;
        }
    }
}