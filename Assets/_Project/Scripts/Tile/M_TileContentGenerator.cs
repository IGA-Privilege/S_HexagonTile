using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;

public class M_TileContentGenerator : MonoBehaviour
{
    private PLR_HexMapTransform m_MapTransform;
    public float radius;
    public float height;

    void Start()
    {
        m_MapTransform = FindObjectOfType<PLR_HexMapTransform>();
    }

    public void GenerateOnSurfaceContent(TileType tileType, Transform tileTrans)
    {
        List< OnTileObject> onTileObjects = new List<OnTileObject> ();
        foreach (var item in m_MapTransform.colorData.hexTiles)
            if (item.tileInfoContainer.tileType == tileType && item.tileInfoContainer.onTileObjects.Length > 0)
            {
                for (int i = 0; i < item.tileInfoContainer.onTileObjects.Length; i++)
                {
                    onTileObjects.Add(item.tileInfoContainer.onTileObjects[i]);
                }
            }

        foreach (OnTileObject item in onTileObjects)
        {
            if (item.toGenRange.maxValue == 0)
            {
                GameObject go = Instantiate(item.targetObj, tileTrans.position, Quaternion.identity);
                go.transform.SetParent(tileTrans.transform.Find("Container"), true);
                ContentVisualization(go.transform, InstantiateType.DropDown);
            }
            else
            {
                int random = Random.Range(item.toGenRange.minValue, item.toGenRange.maxValue);
                for (int i = 0; i < random; i++)
                {
                    GameObject go = Instantiate(item.targetObj, GetRandomPos(tileTrans.position), Quaternion.identity);
                    go.transform.SetParent(tileTrans.transform.Find("Container"), true);
                    float randomScale = Random.Range(item.toScaleRange.minValue, item.toScaleRange.maxValue);
                    ContentVisualization(go.transform, InstantiateType.ScaleUp, randomScale);
                }
            }
        }

        Vector3 GetRandomPos(Vector3 centerPos)
        {
            float zOffset = Random.Range(-radius, radius);
            float xOffset = Random.Range(-radius, radius);
            Vector3 v3Offset = new Vector3(xOffset, 0, zOffset);

          return centerPos+ v3Offset;
        }
    }

    public void ContentVisualization(Transform trans, InstantiateType type)
    {
        trans.Rotate(new Vector3(0, Random.Range(0, 180), 0));
        trans.localScale = Vector3.zero;
        trans.position += new Vector3(0, height, 0);
        Sequence s = DOTween.Sequence();
        s.Append(trans.DOScale(1.2f, 0.4f));
        s.Append(trans.DOScale(0.9f, 0.2f));
        s.Append(trans.DOScale(1, 0.1f));
        s.AppendInterval(0.3f);
        s.Append(trans.DOMoveY(trans.localPosition.y - height, 0.1f));
        s.AppendCallback(() => LandingParticleGeneration());

        void LandingParticleGeneration()
        {
            GameObject go = Instantiate(M_Game.instance.vfx_CircleDust, trans.position, Quaternion.identity);
            Destroy(go, 2);
        }
    }

    public void ContentVisualization(Transform trans, InstantiateType type,float targetScale) 
    {
        trans.Rotate(new Vector3(0, Random.Range(0, 180), 0));
        trans.localScale = Vector3.zero;
        trans.DOScale(targetScale, 1);
    }
}