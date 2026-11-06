using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] Transform[] Spawners;

    private void Start()
    {
        for (int i = 0; i < Spawners.Length; i++)
        {
            Instantiate(_enemyPrefab, Spawners[i].position, Quaternion.identity);
        }
    }
}
