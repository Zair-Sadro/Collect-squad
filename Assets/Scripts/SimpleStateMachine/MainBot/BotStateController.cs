using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotStateController : ASimpleStateController
{
    [SerializeField,Range(0.1f,20)] private float moveSpeed;

    [SerializeField] private Animator animator;
    [SerializeField] private NavMeshAgent navAgent;
    [SerializeField] private TileSetter botTileSetter;
    [SerializeField] private List<TowerBuildPlatform> playerPlatforms = new List<TowerBuildPlatform>();
    [SerializeField] private TileSpawner tileSpawner;

    private bool _inBuildZone;
    private TowerBuildPlatform _towerToBuild;

    #region Properties

    public bool InBuildZone => _inBuildZone;
    public TowerBuildPlatform TowerToBuild => _towerToBuild;
    public Animator Animator => animator;
    public float MoveSpeed => moveSpeed;
    public override BotStateController BotController => this;
    public TileSetter BotTileSetter => botTileSetter;
    public NavMeshAgent NavAgent => navAgent;
    public List<TowerBuildPlatform> PlayerPlatforms => playerPlatforms;
    public TileSpawner TileSpawner => tileSpawner;

    #endregion

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TowerBuildPlatform tower))
        {
            _towerToBuild = tower;
            _inBuildZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out TowerBuildPlatform tower))
        {
            _towerToBuild = null;
            _inBuildZone = false;
        }
    }



}
