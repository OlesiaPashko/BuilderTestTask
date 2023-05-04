namespace BuilderGame.Gameplay.Farming
{
    using DG.Tweening;
    using UnityEngine;

    public class Plant : MonoBehaviour
    {
        [SerializeField] private PlantAnimator plantAnimator;
        [SerializeField] private GameObject doneFxPrefab;

        public void StartGrowing()
        {
            plantAnimator.AnimateGrow().AppendCallback(()=>
            {
                Instantiate(doneFxPrefab, transform);
            });
        }
    }
}