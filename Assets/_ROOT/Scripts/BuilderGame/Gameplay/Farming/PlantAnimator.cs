namespace BuilderGame.Gameplay.Farming
{
    using System.Collections.Generic;
    using DG.Tweening;
    using Unit;
    using UnityEngine;
    using Zenject;

    public class PlantAnimator : MonoBehaviour
    {
        [Inject]
        public Player Player { get; set; }
        
        [Header("Settings")] 
        [SerializeField, Range(0f, 10f)] private float maxDuration;
        [SerializeField, Range(0f, 10f)] private float minDuration;
        
        [Header("Growing")]
        [SerializeField, Range(0f, 1f)] private float startScale = 0.6f;
        [SerializeField] private Ease scaleEase = Ease.OutQuad;
        [SerializeField] private Ease punchEase = Ease.OutQuad;
        [SerializeField] private Vector3 punchValue = new Vector3(0.1f, 0.1f, 0.1f);
        [SerializeField] private float elasticity = 0.3f;
        [SerializeField] private int vibrato = 3;
        
        [Header("Raising")]
        [SerializeField] private Ease scaleDownEase = Ease.OutQuad;
        [SerializeField, Range(0f, 10f)] private float scaleDownDuration = 3f;


        [Header("Prefabs")] 
        [SerializeField] private List<GameObject> plantViews;
        [SerializeField] private GameObject tomatoPrefab;
        [SerializeField] private GameObject appearanceFx;

        private GameObject plant;

        public Sequence AnimateGrow()
        {
            var duration = Random.Range(minDuration, maxDuration);
            return PlantViewAnimation(duration);
        }
        
        public Sequence AnimateRaise()
        {
            var sequence = DOTween.Sequence();
            var tomato = Instantiate(tomatoPrefab, transform);
            sequence.Join(MoveTomato(tomato, Player.centerBone))
                .Join(ScaleDownPlant())
                .AppendCallback(() => Destroy(plant.gameObject));
            return sequence;
        }
        
        private Tween MoveTomato(GameObject tomato, Transform target)
        {
            var duration = 10f;

            var tomatoTransform = tomato.transform;
            float normalizedValue = 0;

            return DOTween.To(() => normalizedValue, newValue =>
            {
                normalizedValue = newValue;
                tomatoTransform.position = Vector3.Lerp(tomatoTransform.position, target.transform.position, normalizedValue);
            }, 1, duration);
        }

        private Tween ScaleDownPlant()
        {
            return plant.transform.DOScale(Vector3.zero, scaleDownDuration)
                .SetEase(scaleDownEase);
        }

        private Sequence PlantViewAnimation(float duration)
        {
            var timePerPlant = duration / plantViews.Count;
            var sequence = DOTween.Sequence();
            foreach (var plantView in plantViews)
            {
                var currentPlant = Instantiate(plantView, transform);
                var startSize = currentPlant.transform.localScale;
                currentPlant.transform.localScale = Vector3.zero;
                sequence
                    .AppendCallback(() => MakeBigPlant(plantView, currentPlant, startSize))
                    .Append(currentPlant.transform.DOScale(startSize, timePerPlant)
                        .SetEase(scaleEase)
                    )
                    .Append(currentPlant.transform.DOPunchScale(punchValue, timePerPlant / 4f, vibrato, elasticity)
                        .SetEase(punchEase))
                    .AppendCallback(() => { DestroyPlant(plantView, currentPlant); });
            }

            return sequence;
        }

        private void DestroyPlant(GameObject plantView, GameObject currentPlant)
        {
            if (plantView == plantViews[^1])
            {
                plant = currentPlant;
                return;
            }

            Destroy(currentPlant.gameObject);
        }

        private void MakeBigPlant(GameObject plantView, GameObject currentPlant, Vector3 startSize)
        {
            currentPlant.transform.localScale = startSize * startScale;
            if (plantView == plantViews[0])
            {
                return;
            }

            Instantiate(appearanceFx, transform);
        }
    }
}