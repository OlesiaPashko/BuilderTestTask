namespace BuilderGame.Gameplay.Farming
{
    using System;
    using DG.Tweening;
    using UnityEngine;

    public class Plant : MonoBehaviour
    {
        [SerializeField] private PlantAnimator plantAnimator;

        public event Action OnPlantReady;

        public void StartGrowing()
        {
            plantAnimator.AnimateGrow().AppendCallback(() => OnPlantReady?.Invoke());
        }
        
        public void Raise()
        {
            plantAnimator.AnimateRaise().AppendCallback(() => Destroy(gameObject));
        }
    }
}