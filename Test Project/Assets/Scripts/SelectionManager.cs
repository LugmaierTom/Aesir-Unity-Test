using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionManager : MonoBehaviour
{
    public delegate void MoveHandler(List<GameObject> items);
    public event MoveHandler _MoveDone;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private float _selectionRange;

    private GameObject _currentSelection;
    private List<GameObject> _selectedItems;


    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        _selectedItems = new List<GameObject>();
    }


    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            SelectItem();
        }
        
        if (Input.GetButtonUp("Fire1"))
        {            
            CheckRow();
            _currentSelection = null;
        }
    }


    private void SelectItem()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        // Guard Clause
        if (hit == false) return;

        IClickable item = hit.transform.GetComponent<IClickable>();

        CheckSelection(hit, item);
    }


    private void CheckSelection(RaycastHit2D hit, IClickable item)
    {
        // If there is no item or it's already selected
        if (item == null || item._isSelected == true) return;

        if (_currentSelection == null)
        {
            AddToSelection(hit, item);
        }
        else if (isInRange(hit) && _currentSelection.tag == hit.transform.tag)
        {
            AddToSelection(hit, item);
        }
    }


    private bool isInRange(RaycastHit2D hit)
    {
        float distance = Vector2.Distance(_currentSelection.transform.position, hit.transform.position);

        if (distance < _selectionRange) return true;

        return false;
    }


    private void AddToSelection(RaycastHit2D hit, IClickable item)
    {
        _currentSelection = hit.transform.gameObject;
        _selectedItems.Add(hit.transform.gameObject);
        item._isSelected = true;
    }


    private void CheckRow()
    {
        if (_selectedItems.Count >= 3)
        {
            _MoveDone?.Invoke(_selectedItems);
        }
        else
        {
            UncheckItems();
        }

        _selectedItems.Clear();
    }


    private void UncheckItems()
    {
        foreach (GameObject selected in _selectedItems)
        {
            selected.GetComponent<IClickable>()._isSelected = false;
        }
    }
}
