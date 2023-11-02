using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_Monsoon : O_ElementBase
{
    private int currentLevel = 2;
    public float height;
    private TileRelativePos currentDirection;
    private ParticleSystem vfx_Monsoon;
    public TileRelativePos initialSide;
    public bool isInitial = false;

    void Start()
    {
        vfx_Monsoon = GetComponentInChildren<ParticleSystem>();
        if (isInitial && transform.parent != null)
        {
            currentTile = transform.parent.parent.Find("Middle");
            currentTile.GetComponentInParent<O_TileInfoContainer>().onTileElements.Add(this);
        }
    }

    protected override void Update()
    {
        base.Update();
        if(currentState == ElementState.OnFunction)
            LevelAdjustment();
    }

    protected override void Locating()
    {
        base.Locating();
        if (currentTile == null) return;

        Vector3 newPos = new Vector3(currentTile.position.x, height, currentTile.position.z);
        transform.position = newPos;
        SideChange(M_Tile.Instance.tile_CurrentSide);
    }

    void SideChange(TileRelativePos targetSide)
    {
        switch (targetSide)
        {
            case TileRelativePos.West:
                transform.DORotate(new Vector3(0, 300, 0), 0.3f);
                currentDirection = TileRelativePos.East;
                break;
            case TileRelativePos.SouthWest:
                transform.DORotate(new Vector3(0, 240, 0), 0.3f);
                currentDirection = TileRelativePos.NorthEast;
                break;
            case TileRelativePos.SouthEast:
                transform.DORotate(new Vector3(0, 180, 0), 0.3f);
                currentDirection = TileRelativePos.NorthWest;
                break;
            case TileRelativePos.East:
                transform.DORotate(new Vector3(0, 120, 0), 0.3f);
                currentDirection = TileRelativePos.West;
                break;
            case TileRelativePos.NorthEast:
                transform.DORotate(new Vector3(0, 60, 0), 0.3f);
                currentDirection = TileRelativePos.SouthWest;
                break;
            case TileRelativePos.NorthWest:
                transform.DORotate(new Vector3(0, 0, 0), 0.3f);
                currentDirection = TileRelativePos.SouthEast;
                break;
        }
    }

    public override void Set()
    {
        base.Set();
    }

    void LevelAdjustment()
    {
        if (currentTile == null) return;
        if (!currentTile.GetComponentInParent<O_TileInfoContainer>().neighborTiles.ContainsKey(currentDirection)) return;

        O_TileInfoContainer[] onRoadTiles = new O_TileInfoContainer[3];
        onRoadTiles[0] = currentTile.GetComponentInParent<O_TileInfoContainer>();
        //Debug.Log("o" + currentTile.parent.name);
        for (int i = 1; i < 3; i++)
        {
            if (!onRoadTiles[i - 1].GetComponentInParent<O_TileInfoContainer>().neighborTiles.ContainsKey(currentDirection)) break;

            onRoadTiles[i] = onRoadTiles[i - 1].neighborTiles[currentDirection];
        }

        currentLevel = 2;
        int maxLevel = 2;
        for (int i = 0; i < onRoadTiles.Length; i++)
        {
            if (onRoadTiles[i] == null) break;

            switch (onRoadTiles[i].thisInfo.tileType)
            {
                case TileType.Start:
                    break;
                case TileType.Destination:
                    break;
                case TileType.Grassland:
                    if (onRoadTiles[i].GetComponent<O_TreeTile>() != null)
                        currentLevel -= onRoadTiles[i].GetComponent<O_TreeTile>().treeLevel;
                    break;
                case TileType.Mountain:
                    if (maxLevel > i+1) maxLevel = i+1;
                    break;
                case TileType.Ocean:
                    break;
                case TileType.FlowerLand:
                    break;
                case TileType.Snow:
                    break;
            }
        }
        if (currentLevel < 1) currentLevel = 1;
        if (currentLevel > maxLevel) currentLevel = maxLevel;

        var particleMainSetting = vfx_Monsoon.main;
        switch (currentLevel)
        {
            case 1:
                particleMainSetting.startLifetime = 1.5f;
                break;
            case 2:
                particleMainSetting.startLifetime = 3f;
                break;
            case 3:
                particleMainSetting.startLifetime = 4f;
                break;
        }

        SendValueToEachTile();

        void SendValueToEachTile()
        {
            //Debug.Log("eneadadad");
            for (int i = 0; i < 3; i++)
            {
                if (onRoadTiles[i] != null)
                {
                    int newLevel = currentLevel - i;
                    if (newLevel < 0) newLevel = 0;

                    onRoadTiles[i].TryAddNewWindLevel(new WindLevelRegister
                    {
                        windLevel = newLevel,
                        forwardDirection = currentDirection,
                        source = this
                    });
                }
            }
        }
    }

    private void OnValidate()
    {
        switch (initialSide)
        {
            case TileRelativePos.West:
                transform.rotation = Quaternion.Euler(new Vector3(0, 300, 0));
                currentDirection = TileRelativePos.East;
                break;
            case TileRelativePos.SouthWest:
                transform.rotation = Quaternion.Euler(new Vector3(0, 240, 0));
                currentDirection = TileRelativePos.NorthEast;
                break;
            case TileRelativePos.SouthEast:
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                currentDirection = TileRelativePos.NorthWest;
                break;
            case TileRelativePos.East:
                transform.rotation = Quaternion.Euler(new Vector3(0, 120, 0));
                currentDirection = TileRelativePos.West;
                break;
            case TileRelativePos.NorthEast:
                transform.rotation = Quaternion.Euler(new Vector3(0, 60, 0));
                currentDirection = TileRelativePos.SouthWest;
                break;
            case TileRelativePos.NorthWest:
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                currentDirection = TileRelativePos.SouthEast;
                break;
        }
        currentState = ElementState.OnFunction;
  
        //Debug.Log(currentTile.GetComponent<O_TileInfoContainer>().thisInfo.tileType);
    }
}
