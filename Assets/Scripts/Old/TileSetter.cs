using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileSetter : MonoBehaviour
{
    [SerializeField] private Transform tilesSpawnerParent;

    [SerializeField] private bool isThisBot = false;
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
    private bool _isGivingTiles;

    private List<Tile> _tiles = new List<Tile>();

    public event Action<int> OnTilesCountChanged;
    public event Action<TowerBuildPlatform> OnBuildZoneEnter;
    public event Action OnBuildZoneExit;


    public bool IsThisBot => isThisBot;
    public List<Tile> Tiles => _tiles;
    public bool IsInBuildZone { get => _isInBuildZone; set => _isInBuildZone = value; }


    private void Start()
    {
        _currentRemovingTime = timeToRemoveTile;
    }

    private void AddTile(Tile tile)
    {
        tile.OnBack();
        tile.transform.SetParent(setupPoint);

        if (_tiles.Count % tilesRow == 0)
            tile.transform.localPosition = new Vector3(0, yOffset * _tiles.Count, 0);
        else
            tile.transform.localPosition = new Vector3(0, yOffset * (_tiles.Count - 1), zOffset);

        tile.transform.localRotation = Quaternion.Euler(Vector3.zero);
        tile.transform.localScale = tilesScale;
        _tiles.Add(tile);
        OnTilesCountChanged?.Invoke(_tiles.Count);
        //add vibration
    }

    public void RemoveTiles(Action towerTileIncrease)
    {
        if(!_isGivingTiles)
            StartCoroutine(RemovingTile(towerTileIncrease));
    }

    private IEnumerator RemovingTile(Action towerTileIncrease)
    {
        _isGivingTiles = true;

        for (int i = _tiles.Count - 1; i >= 0; i--)
        {
            _tiles[i].gameObject.SetActive(false);
            _tiles[i].OnGround();
            _tiles[i].transform.SetParent(tilesSpawnerParent);
            _tiles.Remove(_tiles[i]);
            towerTileIncrease();
            OnTilesCountChanged?.Invoke(_tiles.Count);
            yield return new WaitForSeconds(timeToRemoveTile);
        }

        _isGivingTiles = false;
    }

    public void StopRemovingTiles()
    {
        _isGivingTiles = false;
        StopAllCoroutines();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent(out Tile tile) && _tiles.Count < maxTiles)
            AddTile(tile);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TowerBuildPlatform t))
        {
            OnBuildZoneEnter?.Invoke(t);
            _isInBuildZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out TowerBuildPlatform t))
        {
            _isInBuildZone = false;
            OnBuildZoneExit?.Invoke();
            StopRemovingTiles();
        }
    }


}
