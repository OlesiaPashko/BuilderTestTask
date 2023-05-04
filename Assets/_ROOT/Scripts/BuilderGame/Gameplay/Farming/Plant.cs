namespace BuilderGame.Gameplay.Farming
{
    using System;
    using DG.Tweening;
    using UnityEngine;

    public class Plant : MonoBehaviour
    {
        [SerializeField] private PlantAnimator plantAnimator;
        [SerializeField] private float afterRaiseDelay = 1f;
        [SerializeField] private GameObject doneFxPrefab;

        public event Action OnPlantReady;
        public event Action OnPlantRaised;

        public void StartGrowing()
        {
            plantAnimator.AnimateGrow().AppendCallback(() =>
            {
                Instantiate(doneFxPrefab, transform);
                OnPlantReady?.Invoke();
            });
        }
        
        public void Raise()
        {
            plantAnimator.AnimateRaise().AppendInterval(afterRaiseDelay).AppendCallback(() => OnPlantRaised?.Invoke());
        }
    }
}