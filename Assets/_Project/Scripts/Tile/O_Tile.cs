using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class O_Tile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //OnSeedLanding();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSeedLanding()
    {
        M_Tile.Instance.DetermineNearbyTile(transform);
    }

    public void OnClicked()
    {
        if (M_Tile.Instance.currentTilesNeighbors.Contains(transform.parent)) NearbyTileClicked();
    }

    private void NearbyTileClicked()
    {
        if (M_Seed.Instance.currentLandingTile != this)
        {
            //M_Seed.
        }
    }
}
