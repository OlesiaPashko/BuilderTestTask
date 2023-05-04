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
        //[SerializeField] private GameObject finalFx;
        
        public Sequence Pump()
        {
            var sequence = DOTween.Sequence();
            var pump = transform.DOPunchScale(punch, punchDuration, vibrato, elasticity).SetEase(punchEase);
            
            sequence.Append(pump).Append(Scale())
                .Join(transform.DOLocalMoveY(0.3f, 1f))
                .Join(transform.DOLocalMoveX(Random.Range(-1f,1f), 1f))
                .Join(transform.DOLocalMoveZ(Random.Range(-1f,1f), 1f));
            return sequence;
        }

        private Sequence Scale()
        {
            var sequence = DOTween.Sequence();

            var scale = transform.DOScale(new Vector3(0f, transform.position.y, 0f), scaleDuration)
                .SetEase(scaleEase);
            sequence.Append(scale);//.AppendCallback(() => Instantiate(finalFx, transform.parent));
            return sequence;
        }
    }
}