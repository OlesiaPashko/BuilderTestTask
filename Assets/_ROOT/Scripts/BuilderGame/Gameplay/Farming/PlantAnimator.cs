namespace BuilderGame.Gameplay.Farming
{
    using System.Collections.Generic;
    using DG.Tweening;
    using UnityEngine;

    public class PlantAnimator : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField, Range(0f,10f)] private float maxDuration;
        [SerializeField, Range(0f,10f)] private float minDuration;

        [SerializeField] private List<GameObject> plantViews;

        public void AnimateGrow()
        {
            var duration = Random.Range(minDuration, maxDuration);
            Debug.Log($"<color=red>duration => {duration} </color>");
            PlantViewAnimation(duration);
        }

        private void PlantViewAnimation(float duration)
        {
            var timePerPlant = duration / plantViews.Count;
            var sequence = DOTween.Sequence();
            foreach (var plantView in plantViews)
            {
                var plant = Instantiate(plantView, transform);
                var startSize = plant.transform.localScale;
                plant.transform.localScale = Vector3.zero;
                sequence.AppendCallback(()=>MakeBigPlant(plant, startSize))
                    .Append(plant.transform.DOScale(startSize, timePerPlant))
                    .AppendCallback(()=>
                    {
                        if (plantView == plantViews[^1])
                        {
                            return;
                        }
                        Destroy(plant.gameObject);
                    });
            }
        }

        private void MakeBigPlant(GameObject plant, Vector3 startSize)
        {
            plant.transform.localScale = startSize * 0.6f;
        }
    }
}