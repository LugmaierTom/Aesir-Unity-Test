using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void DestroyHandler();

    [Range(0.1f, 2f)] public float _spawnRate;

    [SerializeField] private int _gameFieldHeight;
    [SerializeField] private SelectionManager _selectionManager;
    [SerializeField] private EndAfterXMoves _gameOverCondition;
    [SerializeField] private GameObject _endScreen;
    [SerializeField] private GameObject[] _itemSpawnPositions;
    [SerializeField] private GameObject[] _items;

    private List<GameObject> _instatiatedObjects;
    private List<GameObject> _itemsToRemove;
    private List<int> _itemsToSpawn;
    private int _idCount;
    private bool _isRemovingItems = false;


    private void Awake()
    {
        _instatiatedObjects = new List<GameObject>();
        _itemsToRemove = new List<GameObject>();
    }

    
    private void Start()
    {
        _selectionManager._MoveDone += MoveDoneHandler;
        StartCoroutine("InitialiseItems");
    }


    private IEnumerator InitialiseItems()
    {
        WaitForSeconds waiting = new WaitForSeconds(_spawnRate);

        for (int i = 0; i < _gameFieldHeight; i++)
        {
            for (int k = 0; k < _itemSpawnPositions.Length; k++)
            {
                SpawnNewItem(k);
            }

            yield return waiting;
        }
    }


    private void SpawnNewItem(int collumn)
    {
        GameObject randomItem = _items[UnityEngine.Random.Range(0, _items.Length)];
        Vector2 position = _itemSpawnPositions[collumn].transform.position;

        var instance = Instantiate(randomItem, position, Quaternion.identity);
        var item = instance.GetComponent<IClickable>();

        item.SetItemProperties(_idCount++, collumn);

        _instatiatedObjects.Add(instance);
    }


    private void MoveDoneHandler(List<GameObject> items)
    {
       if (_gameOverCondition.EndGame())
        {
            _endScreen.SetActive(true);
        }

        _itemsToRemove.AddRange(items);

        if (_isRemovingItems == false)
            StartCoroutine("RemoveItems");
    }


    private IEnumerator RemoveItems()
    {
        _isRemovingItems = true;
        WaitForSeconds waiting = new WaitForSeconds(_spawnRate);
        
        while (_itemsToRemove.Count > 0)
        {
            IClickable itemToFind = _itemsToRemove[0].GetComponent<IClickable>();

            for (int i = 0; i < _instatiatedObjects.Count; i++)
            {
                if (_instatiatedObjects[i].GetComponent<IClickable>()._id == itemToFind._id)
                {
                    Destroy(_instatiatedObjects[i]);
                    _instatiatedObjects.RemoveAt(i);
                    SpawnNewItem(itemToFind._collumn);
                }
            }
            
            _itemsToRemove.RemoveAt(0);
            yield return waiting;
        }

        _isRemovingItems = false;
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }


    public void QuitGane()
    {
        Application.Quit();
    }
}
