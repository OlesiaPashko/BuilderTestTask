namespace BuilderGame.Gameplay.Unit
{
    using Animation;
    using UnityEngine;

    public class Player : MonoBehaviour
    {
        public Transform centerBone;

        [SerializeField] private UnitDiggingAnimation unitDiggingAnimation;

        private void OnValidate()
        {
            unitDiggingAnimation = GetComponent<UnitDiggingAnimation>();
        }

        public void SetDigging()
        {
            unitDiggingAnimation.SetDigging();
            
        }
    }
}