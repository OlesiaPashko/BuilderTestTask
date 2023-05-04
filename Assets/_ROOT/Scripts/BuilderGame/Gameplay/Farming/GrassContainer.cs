namespace BuilderGame.Gameplay.Farming
{
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    public class GrassContainer : MonoBehaviour
    {
        [SerializeField] private int rows = 5;
        [SerializeField] private int columns = 5;
        [SerializeField] private GrassUnit grassUnitPrefab;

        [Inject]
        public DiContainer DiContainer { get; set; }

        private List<GrassUnit> grassUnits = new();
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
                    var grassUnit = DiContainer.InstantiatePrefabForComponent<GrassUnit>(grassUnitPrefab, transform);
                    grassUnit.transform.position = GetCenteredPosition(i, j, grassUnit);
                    grassUnits.Add(grassUnit);
                }
            }
        }

        private Vector3 GetCenteredPosition(int columnIndex, int rowIndex, GrassUnit grassUnit)
        {
            return new Vector3(columnIndex - (columns / 2f) + (grassUnit.Size / 2f), 0,
                rowIndex - (rows / 2f) + (grassUnit.Size / 2f));
        }
    }
}