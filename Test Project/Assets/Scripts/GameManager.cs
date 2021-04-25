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
    [SerializeField] private GameObject[] _itemSpawnPositions;
    [SerializeField] private GameObject[] _items;
    [SerializeField] private GameObject _endScreen;

    private int _idCount;
    private List<GameObject> _instatiatedObjects;


    private void Awake()
    {
        _instatiatedObjects = new List<GameObject>();
    }

    
    void Start()
    {
        _selectionManager._MoveDone += EndGame;
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

    private void EndGame(int score)
    {
       if (_gameOverCondition.EndGame())
        {
            _endScreen.SetActive(true);
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
