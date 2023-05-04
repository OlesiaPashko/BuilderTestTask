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

        private GameObject doneOffspringFx;

        public event Action OnPlantReady;
        public event Action OnPlantRaised;

        public void StartGrowing()
        {
            plantAnimator.AnimateGrow().AppendCallback(() =>
            {
                doneOffspringFx = Instantiate(doneFxPrefab, transform);
                OnPlantReady?.Invoke();
            });
        }
        
        public void Raise()
        {
            Destroy(doneOffspringFx.gameObject);
            plantAnimator.AnimateRaise().AppendInterval(afterRaiseDelay).AppendCallback(() =>
            {
                OnPlantRaised?.Invoke();
            });
        }
    }
}