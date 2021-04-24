using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int _gameFieldHeight;

    [Range(0.1f, 2f)]
    public float _spawnRate;

    [SerializeField]
    private GameObject[] _itemSpawnPositions;

    [SerializeField]
    private GameObject[] _items;

    private int[] columns;


    private void Awake()
    {
        columns = new int[_itemSpawnPositions.Length];
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("InitialiseBalls");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator InitialiseBalls()
    {
        WaitForSeconds waiting = new WaitForSeconds(_spawnRate);

        for (int i = 0; i < _gameFieldHeight; i++)
        {
            for (int k = 0; k < _itemSpawnPositions.Length; k++)
            {
                GameObject selectedItem = _items[Random.Range(0, _items.Length)];
                Vector3 position = _itemSpawnPositions[k].transform.position;

                Instantiate(selectedItem, position, Quaternion.identity);
                columns[k] = i + 1;
            }

            yield return waiting;
        }
    }
}
