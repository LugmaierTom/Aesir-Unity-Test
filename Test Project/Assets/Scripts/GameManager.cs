using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void DestroyHandler();

    public int _gameFieldHeight;
    public List<GameObject> _instatiatedObjects;
    public SelectionManager _selectionManager;

    [Range(0.1f, 2f)] public float _spawnRate;

    [SerializeField] private GameObject[] _itemSpawnPositions;
    [SerializeField] private GameObject[] _items;

    private int _idCount;


    private void Awake()
    {
        _instatiatedObjects = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("InitialiseItems");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            PrintList();
        }
    }

    private void PrintList()
    {
        foreach (GameObject clickable in _instatiatedObjects)
        {
            Debug.Log(clickable.GetComponent<IClickable>()._collumn);
        }
    }

    private IEnumerator InitialiseItems()
    {
        WaitForSeconds waiting = new WaitForSeconds(_spawnRate);

        for (int i = 0; i < _gameFieldHeight; i++)
        {
            for (int k = 0; k < _itemSpawnPositions.Length; k++)
            {
                SpawnNewItem(k);
                //columns[k] = i + 1;
            }

            yield return waiting;
        }

        _selectionManager.SetItemList(_instatiatedObjects);
    }

    private void SpawnNewItem(int collumn)
    {
        GameObject randomItem = _items[UnityEngine.Random.Range(0, _items.Length)];
        Vector2 position = _itemSpawnPositions[collumn].transform.position;

        var instance = Instantiate(randomItem, position, Quaternion.identity);
        var item = instance.GetComponent<IClickable>();

        item._id = _idCount++;
        item._collumn = collumn;
        instance.GetComponent<Item>().Destroyed += SpawnNewItem;

        _instatiatedObjects.Add(instance);
    }
}
