namespace BuilderGame.Gameplay.Farming
{
    using DG.Tweening;
    using UnityEngine;

    public class Plant : MonoBehaviour
    {
        [SerializeField] private PlantAnimator plantAnimator;

        public void StartGrowing()
        {
            plantAnimator.AnimateGrow();
        }
    }
}