namespace BuilderGame.Gameplay.Farming
{
    using UnityEngine;

    public class Plant : MonoBehaviour
    {
        [SerializeField] private Transform model;
        
        public void StartGrowing()
        {
            model.gameObject.SetActive(true);
        }
    }
}