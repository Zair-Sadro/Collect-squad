using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class TileSetter : MonoBehaviour
{
    [SerializeField] private Transform tilesSpawnerParent;
    
    [Header("Add to Unit Settings")]
    [SerializeField] private Transform setupPoint;
    [SerializeField] private Vector3 tilesScale;
    [SerializeField] private float tileSetSpeed;
    [SerializeField, Range(0, 100)] private float zOffset;
    [SerializeField, Range(0, 100)] private float yOffset = 0;
    [SerializeField] private int tilesRow = 2;
    [Space]
    [SerializeField] private float timeToRemoveTile;

    [Header("Bot Settings")]
    [SerializeField] private bool isThisBot = false;
    [SerializeField] private int maxTiles;
    [SerializeField] private TowerBuildPlatform desiredTowerToBuild;

    private float _currentRemovingTime;
    private bool _isInBuildZone;
    private bool _isGivingTiles;

    private UserData _playerData;
    private Tile _lastSetTile;
    private TowerBuildPlatform _currentTowerPlatform;

    private List<Tile> _tiles = new List<Tile>();

    public event Action<int> OnTilesCountChanged;
    public event Action<TowerBuildPlatform> OnBuildZoneEnter;
    public event Action OnBuildZoneExit;
    public event Action<SwapZone> OnBombZoneEnter;
    public event Action OnBombZoneExit;



    public bool IsThisBot => isThisBot;
    public List<Tile> Tiles => _tiles;
    public bool IsInBuildZone { get => _isInBuildZone; set => _isInBuildZone = value; }


    private void Start()
    {
        _currentRemovingTime = timeToRemoveTile;
        _playerData = GameController.Data;
    }

    private int GetMaxTiles()
    {
        return isThisBot ? maxTiles : _playerData.MaxTiles;
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

      
    }

    public void RemoveTiles(Action towerTileIncrease, TowerBuildPlatform tower)
    {
        if(isThisBot)
        {
            if (desiredTowerToBuild != null && desiredTowerToBuild == tower)
            {
                if (!_isGivingTiles)
                    StartCoroutine(RemovingTile(towerTileIncrease));
            }
        }
        else
        {
            if (!_isGivingTiles)
                StartCoroutine(RemovingTile(towerTileIncrease));
        }
    }

    private IEnumerator RemovingTile(Action towerTileIncrease)
    {
        _isGivingTiles = true;
        for (int i = _tiles.Count - 1; i >= 0; i--)
        {
            _lastSetTile = _tiles[i];

            if(_lastSetTile != null && _currentTowerPlatform != null)
                _lastSetTile.transform.DOMove(_currentTowerPlatform.transform.position, tileSetSpeed);


            yield return new WaitForSeconds(timeToRemoveTile);

            _tiles[i].gameObject.SetActive(false);
            _tiles[i].OnGround();
            _tiles[i].transform.SetParent(tilesSpawnerParent);
            _tiles.Remove(_tiles[i]);
            towerTileIncrease();
            OnTilesCountChanged?.Invoke(_tiles.Count);

            if (!isThisBot)
                Vibration.Vibrate(25);
        }
        _isGivingTiles = false;
    }

    public void StopRemovingTiles()
    {
        if(_lastSetTile != null)
        {
            _lastSetTile.gameObject.SetActive(false);
            _lastSetTile.OnGround();
            _lastSetTile.transform.SetParent(tilesSpawnerParent);
            _tiles.Remove(_lastSetTile);
            OnTilesCountChanged?.Invoke(_tiles.Count);
        }

        _isGivingTiles = false;
       // RebuildTiles();
        StopAllCoroutines();
    }

    private void RebuildTiles()
    {
        for (int i = 0; i < _tiles.Count; i++)
        {
            if(_tiles.Count % tilesRow == 0)
            {
                var evenTile = _tiles[i];
            }
            else
            {
                var notEvenTile = _tiles[i];
            }
        }
    }    

    public void RemoveTilesAtCount(int count)
    {
        if (_tiles.Count <= 0 || count > _tiles.Count)
            return;

        for (int i = 0; i < count; i++)// dont like this
        {
            for (int j = 0; j < _tiles.Count; j++)
            {
                _tiles[j].gameObject.SetActive(false);
                _tiles[j].OnGround();
                _tiles[j].transform.SetParent(tilesSpawnerParent);
                _tiles.Remove(_tiles[j]);
                OnTilesCountChanged?.Invoke(_tiles.Count);
            }
        }

    }

    private void OnTowerSelfDestroy()
    {
        StopRemovingTiles();
        _currentTowerPlatform.DestroyEvent.RemoveAllListeners();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Tile tile) && _tiles.Count < GetMaxTiles())
        {
            if (!isThisBot)
                Vibration.Vibrate(25);

            AddTile(tile);
        }

        if (other.TryGetComponent(out TowerBuildPlatform t))
        {
            _currentTowerPlatform = t;
            _currentTowerPlatform.OnTowerBuild += OnBuild;
            _currentTowerPlatform.DestroyEvent.AddListener(OnTowerSelfDestroy);
        }
    }

    private void OnBuild(TowerBuildPlatform obj)
    {
        OnBuildZoneExit?.Invoke();
        _isGivingTiles = false;
        _isInBuildZone = false;
        StopAllCoroutines();
        _currentTowerPlatform.OnTowerBuild -= OnBuild;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out TowerBuildPlatform t))
        {
            _currentTowerPlatform = t;
            OnBuildZoneEnter?.Invoke(t);
            _isInBuildZone = true;
        }

        if (other.TryGetComponent(out SwapZone swapZone))
            OnBombZoneEnter?.Invoke(swapZone);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out TowerBuildPlatform t))
        {
            _isInBuildZone = false;
            OnBuildZoneExit?.Invoke();
            StopRemovingTiles();
        }

        if (_currentTowerPlatform != null)
        {
            _currentTowerPlatform.OnTowerBuild -= OnBuild;
            _currentTowerPlatform.DestroyEvent.RemoveAllListeners();

            _currentTowerPlatform = null;
        }

        if (other.TryGetComponent(out SwapZone swapZone))
            OnBombZoneExit?.Invoke();
    }

    public TowerBuildPlatform SetDiseredBuild(TowerBuildPlatform platform)
    {
        return desiredTowerToBuild = platform;
    }
}
