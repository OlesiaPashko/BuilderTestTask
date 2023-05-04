namespace BuilderGame.Gameplay.Farming
{
    using System.Collections.Generic;
    using DG.Tweening;
    using UnityEngine;

    public class PlantAnimator : MonoBehaviour
    {
        [Header("Settings")] [SerializeField, Range(0f, 10f)]
        private float maxDuration;

        [SerializeField, Range(0f, 10f)] private float minDuration;
        [SerializeField, Range(0f, 1f)] private float startScale = 0.6f;
        [SerializeField] private Ease scaleEase = Ease.OutQuad;
        [SerializeField] private Ease punchEase = Ease.OutQuad;
        [SerializeField] private Vector3 punchValue = new Vector3(0.1f, 0.1f, 0.1f);
        [SerializeField] private float elasticity = 0.3f;
        [SerializeField] private int vibrato = 3;

        [Header("Prefabs")] [SerializeField] private List<GameObject> plantViews;
        [SerializeField] private GameObject appearanceFx;

        public Sequence AnimateGrow()
        {
            var duration = Random.Range(minDuration, maxDuration);
            return PlantViewAnimation(duration);
        }

        private Sequence PlantViewAnimation(float duration)
        {
            var timePerPlant = duration / plantViews.Count;
            var sequence = DOTween.Sequence();
            foreach (var plantView in plantViews)
            {
                var plant = Instantiate(plantView, transform);
                var startSize = plant.transform.localScale;
                plant.transform.localScale = Vector3.zero;
                sequence
                    .AppendCallback(() => MakeBigPlant(plantView, plant, startSize))
                    .Append(plant.transform.DOScale(startSize, timePerPlant)
                        .SetEase(scaleEase)
                    )
                    .Append(plant.transform.DOPunchScale(punchValue, timePerPlant / 2f, vibrato, elasticity)
                        .SetEase(punchEase))
                    .AppendCallback(() => { DestroyPlant(plantView, plant); });
            }

            return sequence;
        }

        private void DestroyPlant(GameObject plantView, GameObject plant)
        {
            if (plantView == plantViews[^1])
            {
                return;
            }

            Destroy(plant.gameObject);
        }

        private void MakeBigPlant(GameObject plantView, GameObject plant, Vector3 startSize)
        {
            plant.transform.localScale = startSize * startScale;
            if (plantView == plantViews[0])
            {
                return;
            }

            Instantiate(appearanceFx, transform);
        }
    }
}