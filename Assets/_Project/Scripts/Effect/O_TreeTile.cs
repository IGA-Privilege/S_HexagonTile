using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class O_TreeTile : MonoBehaviour
{
    [HideInInspector] public int treeLevel = 0;
    [SerializeField] private Transform[] registTrees = new Transform[3];
    public int initialTreeNumber;
    private bool isInitialized = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (M_Game.instance.isGameStart && !isInitialized && initialTreeNumber > 0)
        {
            InitializeTree();
            isInitialized = true;
        }
    }

    public void Growing()
    {
        treeLevel++;
        if (treeLevel > 3) treeLevel = 3;
        if (registTrees[treeLevel-1] == null)
        {
            Transform trans= Instantiate(M_Game.instance.pre_Tree, transform.position, Quaternion.identity).transform;
            trans.position = GetRandomPos(transform.position);
            trans.SetParent(transform.Find("Container"), true);

            float randomScale = Random.Range(0.9f, 1.2f);
            trans.Rotate(new Vector3(0, Random.Range(0, 180), 0));
            trans.localScale = Vector3.zero;
            trans.DOScale(randomScale, 1);
            registTrees[treeLevel - 1] = trans;
        }

        Vector3 GetRandomPos(Vector3 centerPos)
        {
            float zOffset = Random.Range(-2, 2);
            float xOffset = Random.Range(-2, 2);
            Vector3 v3Offset = new Vector3(xOffset, 0, zOffset);

            return centerPos + v3Offset;
        }
    }

    private void InitializeTree()
    {
        for (int i = 0; i < initialTreeNumber; i++)
        {
            Growing();
        }
    }
}
