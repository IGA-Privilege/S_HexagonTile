using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class M_Game : MonoBehaviour
{
    public static M_Game instance;
    public GameObject vfx_CircleDust;
    public GameObject pre_Monsoon;
    public GameObject pre_Tree;
    public GameObject pre_Cloud;
    public GameObject pre_Sun;
    public GameObject pre_Boat;
    public GameObject pre_Bird;
    public Color color_GrassLand;

    public bool isGameStart = false;

    public GameObject seed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(this.gameObject);
    }

    private void Update()
    {
        if (isGameStart && !seed.activeInHierarchy)
        {
            seed.SetActive(true);
        }
    }

    public void GameSucceed()
    {
        M_Global.Instance.EnterSwitchScene(1);
    }

    public void GameFailed()
    {
        M_Global.Instance.EnterSwitchScene(1);
    }

    public void EnterResultChecking()
    {
        StartCoroutine(StartCheck());
    }

    IEnumerator StartCheck()
    {
        yield return new WaitForSeconds(1);
        O_TileInfoContainer currentTile = FindObjectOfType<M_SeedAction>().tile_Landing.GetComponentInParent<O_TileInfoContainer>();
        if (currentTile.thisInfo.tileType == TileType.Destination) GameSucceed();
        else GameFailed();
    }
}
