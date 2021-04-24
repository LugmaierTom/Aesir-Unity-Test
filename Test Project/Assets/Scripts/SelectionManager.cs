using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectionManager : MonoBehaviour
{
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
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            SelectItem();
        }
        
        if (Input.GetButtonUp("Fire1"))
        {
            UncheckItems();
            _selectedItems.Clear();
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
        if (item != null && item._isSelected == false)
        {
            if (_currentSelection == null)
            {
                _currentSelection = hit.transform.gameObject;
                _selectedItems.Add(hit.transform.gameObject);
                item._isSelected = true;
                Debug.Log(_selectedItems.Count);
            }
            else if (_currentSelection.tag == hit.transform.tag)
            {
                _selectedItems.Add(hit.transform.gameObject);
                item._isSelected = true;
                Debug.Log(_selectedItems.Count);
            }
        }
    }

    private void UncheckItems()
    {
        foreach (GameObject selected in _selectedItems)
        {
            selected.GetComponent<IClickable>()._isSelected = false;
        }
    }
}
