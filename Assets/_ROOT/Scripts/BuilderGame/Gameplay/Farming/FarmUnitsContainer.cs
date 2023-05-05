namespace BuilderGame.Gameplay.Farming
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using Zenject;

    public class FarmUnitsContainer : MonoBehaviour
    {
        [Inject]
        public DiContainer DiContainer { get; set; }

        [Inject]
        public IFarmField FarmField { get; set; }
        
        [SerializeField] private int rows = 5;
        [SerializeField] private int columns = 5;
        [SerializeField] private FarmUnit farmUnitPrefab;

        private List<FarmUnit> farmUnits = new();
        private void Start()
        {
            SpawnGrassUnits();
        }

        private void SpawnGrassUnits()
        {
            for (int i = 0; i < columns; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    var farmUnit = DiContainer.InstantiatePrefabForComponent<FarmUnit>(farmUnitPrefab, transform);
                    farmUnit.transform.position = GetCenteredPosition(i, j, farmUnit);
                    farmUnit.OnStateUpdated += OnUnitStateUpdated;
                    farmUnits.Add(farmUnit);
                }
            }
        }

        private void OnUnitStateUpdated()
        {
            if (farmUnits.All(x => x.State == FarmUnitState.Ground))
            {
                FarmField.SetPlanting();
            }
        }

        private Vector3 GetCenteredPosition(int columnIndex, int rowIndex, FarmUnit farmUnit)
        {
            var offset = new Vector3(columnIndex - (columns / 2f) + (farmUnit.Size / 2f), 0,
                rowIndex - (rows / 2f) + (farmUnit.Size / 2f));
            return transform.position + offset;
        }
    }
}