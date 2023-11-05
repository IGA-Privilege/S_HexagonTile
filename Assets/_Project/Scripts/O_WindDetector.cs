using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_WindDetector : MonoBehaviour
{
    public enum SeedState { Floating,Moving}
    private Transform tile_Landing;
    private O_Monsoon initialSource = null;
    private M_SeedAction seedAction;
    [HideInInspector] public SeedState currentState = SeedState.Floating;
    private M_FloatingSeed seedFloating;

    private void Start()
    {
        tile_Landing = GetComponent<M_SeedAction>().tile_Landing;
        seedAction = GetComponent<M_SeedAction>();
        seedFloating = GetComponent<M_FloatingSeed>();
    }

    private void Update()
    {
        if(currentState!=SeedState.Moving)
        CheckWhetherThereIsMonsoon();
    }

    private void CheckWhetherThereIsMonsoon()
    {
        if (tile_Landing.GetComponentInParent<O_TileInfoContainer>().onTileWinds.Count > 0)
        {
            if (GetStrongestWind() != null)
            {
                WindLevelRegister tempDate = GetStrongestWind();
                if (tempDate.windLevel > 0)
                {
                    seedAction.TryFreeMove(seedAction.tile_Landing.GetComponent<O_TileInfoContainer>().neighborTiles[GetStrongestWind().forwardDirection].transform);
                    currentState = SeedState.Moving;
                }
            }
        }
        WindLevelRegister GetStrongestWind()
        {
            WindLevelRegister tempWindData = new WindLevelRegister
            {
                windLevel = 0,
                forwardDirection = TileRelativePos.West,
                source = null,
            };

            if (initialSource == null)
            {
                tempWindData = tile_Landing.GetComponentInParent<O_TileInfoContainer>().onTileWinds[0];
                initialSource = tempWindData.source;
            }
            else
            {
                foreach (WindLevelRegister wind in tile_Landing.GetComponentInParent<O_TileInfoContainer>().onTileWinds)
                {
                    if (wind.source == initialSource)
                    {
                        tempWindData = wind;
                        initialSource = tempWindData.source;
                    }
                }
            }

            foreach (WindLevelRegister wind in tile_Landing.GetComponentInParent<O_TileInfoContainer>().onTileWinds)
            {
                if (tempWindData.windLevel < wind.windLevel)
                {
                    tempWindData = wind;
                    initialSource = tempWindData.source;
                }
            }

            if (tempWindData.source == null) return null;
            else return tempWindData;
        }
    }

    public void Recheck()
    {
        CheckWhetherThereIsMonsoon();
    }

}
