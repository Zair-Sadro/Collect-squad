using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileSetter : MonoBehaviour
{
    [Header("Add to Unit Settings")]
    [SerializeField] private Transform setupPoint;
    [SerializeField] private Vector3 tilesScale;
    [SerializeField, Range(0, 100)] private float zOffset;
    [SerializeField, Range(0, 100)] private float yOffset = 0;
    [SerializeField] private int tilesRow = 2;
    [SerializeField] private int maxTiles;
    [Space]
    [SerializeField] private float timeToRemoveTile;

    private float _currentRemovingTime;
    private bool _isInBuildZone;

    private List<GameObject> tiles = new List<GameObject>();

    public event Action<int> OnTilesCountChanged;

    public bool IsInBuildZone { get => _isInBuildZone; set => _isInBuildZone = value; }


    private void Start()
    {
        _currentRemovingTime = timeToRemoveTile;
    }

    private void AddTile(GameObject plate)
    {
        plate.transform.SetParent(setupPoint);
        if (tiles.Count % tilesRow == 0)
            plate.transform.localPosition = new Vector3(0, yOffset * tiles.Count, 0);
        else
            plate.transform.localPosition = new Vector3(0, yOffset * (tiles.Count - 1), zOffset);

        plate.transform.localRotation = Quaternion.Euler(Vector3.zero);
        plate.transform.localScale = tilesScale;
        tiles.Add(plate.gameObject);
        OnTilesCountChanged?.Invoke(tiles.Count);
        
        //add vibration

    }
    public void RemoveTiles()
    {
        RemovingTile();
    }

    private void RemovingTile()
    {
        while(_isInBuildZone && tiles.Count > 0)
        {
            if(_currentRemovingTime <= 0)
            {
                Destroy(tiles[tiles.Count - 1]);
                tiles.RemoveAt(tiles.Count - 1);
                OnTilesCountChanged?.Invoke(tiles.Count);
                _currentRemovingTime = timeToRemoveTile;
            }
            else
                _currentRemovingTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerPlate") && tiles.Count < maxTiles)
            AddTile(other.gameObject);


        if (other.TryGetComponent(out Test t))
            _isInBuildZone = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Test t))
        {
            _isInBuildZone = false;
            StopAllCoroutines();
        }
    }


}
