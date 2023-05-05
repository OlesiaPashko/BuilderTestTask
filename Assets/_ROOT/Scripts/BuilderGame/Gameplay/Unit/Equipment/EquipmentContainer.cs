namespace BuilderGame.Gameplay.Unit.Equipment
{
    using DG.Tweening;
    using UnityEngine;

    public class EquipmentContainer : MonoBehaviour
    {
        [SerializeField] private GameObject shovel;

        private GameObject spawnedEquipment;

        private Tween currentTween;

        public void AddShovel()
        {
            currentTween?.Kill();
            if (spawnedEquipment != null)
            {
                Destroy(spawnedEquipment.gameObject);
            }
            spawnedEquipment = Instantiate(shovel, transform);
            spawnedEquipment.transform.localScale = Vector3.zero;
            currentTween = spawnedEquipment.transform.DOScale(Vector3.one, 0.35f).SetEase(Ease.OutQuint);
        }
        
        public void RemoveShovel()
        {
            currentTween?.Kill();
            if (spawnedEquipment == null)
            {
                return;
            }
            currentTween = DOTween.Sequence().Append(spawnedEquipment.transform.DOScale(Vector3.zero, 0.6f))
                .SetEase(Ease.OutQuint)
                .AppendCallback(()=>Destroy(spawnedEquipment.gameObject));
        }
    }
}