using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using static UnityEditor.Progress;

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
        List<OnTileObject> onTileObjects = new List<OnTileObject>();
        bool isSoloGen = false;
        foreach (var item in m_MapTransform.colorData.hexTiles)
            if (item.tileInfoContainer.tileType == tileType && item.tileInfoContainer.onTileObjects.Length > 0)
            {
                isSoloGen = item.tileInfoContainer.isSoloGen;
                for (int i = 0; i < item.tileInfoContainer.onTileObjects.Length; i++)
                    onTileObjects.Add(item.tileInfoContainer.onTileObjects[i]);
            }

        if (isSoloGen)
        {
            int random = Random.Range(0, onTileObjects.Count);

            GameObject go = Instantiate(onTileObjects[random].targetObj, tileTrans.position + new Vector3(0, 1, 0), Quaternion.identity);
            go.transform.SetParent(tileTrans.transform.Find("Container"), true);
            float randomScale = Random.Range(onTileObjects[random].toScaleRange.minValue, onTileObjects[random].toScaleRange.maxValue);
            SoloContentVisualization(go.transform, onTileObjects[random].instantiateType, randomScale);
        }
        else
        {
            foreach (OnTileObject item in onTileObjects)
            {
                int random = Random.Range(item.toGenRange.minValue, item.toGenRange.maxValue);
                for (int i = 0; i < random; i++)
                {
                    GameObject go = Instantiate(item.targetObj, GetRandomPos(tileTrans.position) + new Vector3(0, 1, 0), Quaternion.identity);
                    go.transform.SetParent(tileTrans.transform.Find("Container"), true);
                    float randomScale = Random.Range(item.toScaleRange.minValue, item.toScaleRange.maxValue);
                    MultiContentVisualization(go.transform, item.instantiateType, randomScale);
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

    public void SoloContentVisualization(Transform trans, InstantiateType type,float targetScale)
    {
        if(type == InstantiateType.DropDown)
        {
            trans.Rotate(new Vector3(0, Random.Range(0, 180), 0));
            trans.localScale = Vector3.zero;
            trans.position += new Vector3(0, height, 0);
            Sequence s = DOTween.Sequence();
            s.Append(trans.DOScale(targetScale * 1.2f, 0.4f));
            s.Append(trans.DOScale(targetScale * 0.9f, 0.2f));
            s.Append(trans.DOScale(targetScale, 0.1f));
            s.AppendInterval(0.3f);
            s.Append(trans.DOMoveY(trans.localPosition.y - height, 0.1f));
            s.AppendCallback(() => LandingParticleGeneration());
        }
        else if(type == InstantiateType.ScaleUp)
        {
            trans.Rotate(new Vector3(0, Random.Range(0, 180), 0));
            trans.localScale = Vector3.zero;
            trans.DOScale(targetScale, 1).OnComplete(() => M_Game.instance.isGameStart = true);
        }

        void LandingParticleGeneration()
        {
            GameObject go = Instantiate(M_Game.instance.vfx_CircleDust, trans.position, Quaternion.identity);
            Destroy(go, 2);
        }
    }

    public void MultiContentVisualization(Transform trans, InstantiateType type,float targetScale) 
    {
        if (type == InstantiateType.DropDown)
        {
            trans.Rotate(new Vector3(0, Random.Range(0, 180), 0));
            trans.localScale = Vector3.zero;
            trans.position += new Vector3(0, height, 0);
            Sequence s = DOTween.Sequence();
            s.Append(trans.DOScale(targetScale * 1.2f, 0.4f));
            s.Append(trans.DOScale(targetScale * 0.9f, 0.2f));
            s.Append(trans.DOScale(targetScale, 0.1f));
            s.AppendInterval(0.3f);
            s.Append(trans.DOMoveY(trans.localPosition.y - height, 0.1f));
        }
        else if (type == InstantiateType.ScaleUp)
        {
            trans.Rotate(new Vector3(0, Random.Range(0, 180), 0));
            trans.localScale = Vector3.zero;
            trans.DOScale(targetScale, 1);
        }
    }
}