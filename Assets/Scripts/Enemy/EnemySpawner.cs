using Sirenix.OdinInspector;
using UnityEngine;

namespace Enemy
{
    public enum EnemyType
    {
        Duck,
        Horse
    }

    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject horsePrefab;
        [SerializeField] private GameObject duckPrefab;

        public int horseSpawnCount = 20;
        public int duckSpawnCount = 5;

        private Bounds[] spawnAreas;
        
        
        [Button]
        private void DebugSpawnHorses()
        {
            SpawnHorses(horseSpawnCount);
        }
        
        [Button]
        private void DebugSpawnDucks()
        {
            SpawnDucks(duckSpawnCount);
        }

        private void Awake()
        {
            Collider[] colliders = GetComponentsInChildren<Collider>(true);
            spawnAreas = new Bounds[colliders.Length];
            for (int i = 0; i < colliders.Length; i++)
            {
                spawnAreas[i] = colliders[i].bounds;
            }
        }

        public void SpawnHorses(int amount)
        {
            while (amount > 0)
            {
                Bounds spawnArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
                int randomSpawnCount = Random.Range(1, amount);

                for (int i = 0; i < randomSpawnCount; i++)
                {
                    Vector3 position = new Vector3(
                        Random.Range(spawnArea.min.x, spawnArea.max.x),
                        spawnArea.min.y,
                        Random.Range(spawnArea.min.z, spawnArea.max.z)
                    );
                    
                    Instantiate(horsePrefab, position, Quaternion.identity);
                }

                amount -= randomSpawnCount;
            }
        }

        public void SpawnDucks(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Bounds spawnArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
                
                Vector3 position = new Vector3(
                    Random.Range(spawnArea.min.x, spawnArea.max.x),
                    spawnArea.min.y,
                    Random.Range(spawnArea.min.z, spawnArea.max.z)
                );
                
                Instantiate(duckPrefab, position, Quaternion.identity);
            }
        }
    }
}