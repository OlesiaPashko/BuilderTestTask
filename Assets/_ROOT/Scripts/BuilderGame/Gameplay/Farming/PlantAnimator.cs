namespace BuilderGame.Gameplay.Farming
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DG.Tweening;
    using Unit;
    using UnityEngine;
    using Zenject;

    public class PlantAnimator : MonoBehaviour
    {
        [Inject]
        public DiContainer DiContainer { get; set; }
        
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
        [SerializeField, Range(0f, 10f)] private float offspringSpawnDelay = 0.3f;


        [Header("Prefabs")] 
        [SerializeField] private List<GameObject> plantViews;
        [SerializeField] private Offspring offspringPrefab;
        [SerializeField] private GameObject appearanceFx;

        private GameObject plantModel;

        public Sequence AnimateGrow()
        {
            var duration = Random.Range(minDuration, maxDuration);
            return PlantViewAnimation(duration);
        }
        
        public Sequence AnimateRaise()
        {
            var sequence = DOTween.Sequence();
            RaiseOffspring();
            sequence.Join(ScaleDownPlant())
                .AppendCallback(() => Destroy(plantModel.gameObject));
            return sequence;
        }

        private async void RaiseOffspring()
        {
            const int millisecondsInSecond = 1000;
            await Task.Delay((int)(offspringSpawnDelay * millisecondsInSecond));
            var offspring = DiContainer.InstantiatePrefabForComponent<Offspring>(offspringPrefab, transform);
            offspring.Raise();
        }

        private Tween ScaleDownPlant()
        {
            return plantModel.transform.DOScale(Vector3.zero, scaleDownDuration)
                .SetEase(scaleDownEase);
        }

        private Sequence PlantViewAnimation(float duration)
        {
            var timePerPlant = duration / (2 * plantViews.Count);
            var sequence = DOTween.Sequence();
            foreach (var plantView in plantViews)
            {
                var currentPlant = Instantiate(plantView, transform.position, Quaternion.Euler(new Vector3(Random.Range(-10f, 10f), Random.Range(0f, 180f), Random.Range(-10f, 10f))), transform);
                var startSize = currentPlant.transform.localScale;
                currentPlant.transform.localScale = Vector3.zero;
                var size = startSize * startScale;
                sequence
                    .AppendCallback(() => MakeBigPlant(plantView, currentPlant, size))
                    .Append(currentPlant.transform.DOScale(startSize, timePerPlant)
                        .SetEase(scaleEase)
                    )
                    .Append(ScaleToNextForm(plantView, currentPlant, size, timePerPlant))
                    .AppendCallback(() => { DestroyPlant(plantView, currentPlant); });
            }

            return sequence;
        }

        private Tween ScaleToNextForm(GameObject plantView, GameObject currentPlant, Vector3 size, float timePerPlant)
        {
            if (plantView == plantViews[^1])
            {
                plantModel = currentPlant;
                return DOTween.Sequence();
            }

            return currentPlant.transform.DOScale(size, timePerPlant)
                .SetEase(scaleEase);
        }

        private void DestroyPlant(GameObject plantView, GameObject currentPlant)
        {
            if (plantView == plantViews[^1])
            {
                plantModel = currentPlant;
                return;
            }

            Destroy(currentPlant.gameObject);
        }

        private void MakeBigPlant(GameObject plantView, GameObject currentPlant, Vector3 size)
        {
            currentPlant.transform.localScale = size;
            if (plantView == plantViews[0])
            {
                return;
            }

            Instantiate(appearanceFx, transform);
        }
    }
}