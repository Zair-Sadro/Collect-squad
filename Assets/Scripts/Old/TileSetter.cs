using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileSetter : MonoBehaviour
{
    [SerializeField] private Transform tilesSpawnerParent;

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

    private List<Tile> tiles = new List<Tile>();

    public event Action<int> OnTilesCountChanged;

    public bool IsInBuildZone { get => _isInBuildZone; set => _isInBuildZone = value; }


    private void Start()
    {
        _currentRemovingTime = timeToRemoveTile;
    }

    private void AddTile(Tile tile)
    {
        tile.OnBack();
        tile.transform.SetParent(setupPoint);

        if (tiles.Count % tilesRow == 0)
            tile.transform.localPosition = new Vector3(0, yOffset * tiles.Count, 0);
        else
            tile.transform.localPosition = new Vector3(0, yOffset * (tiles.Count - 1), zOffset);

        tile.transform.localRotation = Quaternion.Euler(Vector3.zero);
        tile.transform.localScale = tilesScale;
        tiles.Add(tile);
        OnTilesCountChanged?.Invoke(tiles.Count);
        //add vibration
    }

    public void RemoveTiles()
    {
        StartCoroutine(RemovingTile());
    }

    private IEnumerator RemovingTile()
    {
        for (int i = tiles.Count - 1; i >= 0; i--)
        {
            tiles[i].gameObject.SetActive(false);
            tiles[i].OnGround();
            tiles[i].transform.SetParent(tilesSpawnerParent);
            tiles.Remove(tiles[i]);
            //add tiles to tower
            OnTilesCountChanged?.Invoke(tiles.Count);
            yield return new WaitForSeconds(timeToRemoveTile);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent(out Tile tile) && tiles.Count < maxTiles)
            AddTile(tile);
    }

    private void OnTriggerEnter(Collider other)
    {

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
