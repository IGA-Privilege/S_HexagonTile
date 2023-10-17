using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_TileInteraction : MonoBehaviour
{

    void OnSeedLanding()
    {
        //M_Tile.Instance.DetermineNearbyTile(transform);
    }

    public void OnClicked()
    {
        if(M_Tile.Instance.isMoveAllowed)
        {
            Dictionary<TileRelativePos, O_TileInfoContainer> thisNeighbors = GetComponent<O_TileInfoContainer>().neighborTiles;
            O_TileInfoContainer currentLandingTile = M_SeedAction.Instance.tile_Landing.GetComponent<O_TileInfoContainer>();
            if (thisNeighbors.ContainsValue(currentLandingTile))
            {
                M_SeedAction.Instance.TryRegularMove(transform);
            }

        }

        foreach (var item in GetComponent<O_TileInfoContainer>().neighborTiles)
        {
            Debug.Log(item.Key + " - " + item.Value.thisInfo.tileName);
        }
    }
}
