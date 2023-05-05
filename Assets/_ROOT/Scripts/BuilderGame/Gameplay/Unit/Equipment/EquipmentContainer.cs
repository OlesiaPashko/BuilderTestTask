namespace BuilderGame.Gameplay.Unit.Equipment
{
    using DG.Tweening;
    using UnityEngine;

    public class EquipmentContainer : MonoBehaviour
    {
        [SerializeField] private GameObject shovel;

        private GameObject spawnedEquipment;

        public void AddShovel()
        {
            spawnedEquipment = Instantiate(shovel, transform);
            spawnedEquipment.transform.localScale = Vector3.zero;
            spawnedEquipment.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuint);
        }
        
        public void RemoveShovel()
        {
            if (spawnedEquipment == null)
            {
                return;
            }
            DOTween.Sequence().Append(spawnedEquipment.transform.DOScale(Vector3.zero, 0.3f)).SetEase(Ease.OutQuint)
                .AppendCallback(()=>Destroy(spawnedEquipment.gameObject));
        }
    }
}