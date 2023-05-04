namespace BuilderGame.Gameplay.Farming
{
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    public class FarmUnitsSpawner : MonoBehaviour
    {
        [SerializeField] private int rows = 5;
        [SerializeField] private int columns = 5;
        [SerializeField] private FarmUnit farmUnitPrefab;

        [Inject]
        public DiContainer DiContainer { get; set; }

        private List<FarmUnit> grassUnits = new();
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
                    grassUnits.Add(farmUnit);
                }
            }
        }

        private Vector3 GetCenteredPosition(int columnIndex, int rowIndex, FarmUnit farmUnit)
        {
            return new Vector3(columnIndex - (columns / 2f) + (farmUnit.Size / 2f), 0,
                rowIndex - (rows / 2f) + (farmUnit.Size / 2f));
        }
    }
}