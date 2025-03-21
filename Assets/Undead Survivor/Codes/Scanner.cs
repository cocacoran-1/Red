using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;

    public List<GameObject> targets = new List<GameObject>();
    public GameObject[] sortedTargets;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void FixedUpdate()
    {
        if (!gameManager.isLive || targets.Count > 100)
        {
            return;
        }
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);

        targets.Clear();

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.gameObject.activeSelf)
                targets.Add(hit.transform.gameObject);
        }
        SortTargetsByDistance();
    }
    void SortTargetsByDistance()
    {
        sortedTargets = targets.ToArray();
        for (int i = 0; i < sortedTargets.Length; i++)
        {
            for (int j = i + 1; j < sortedTargets.Length; j++)
            {
                if (Vector3.Distance(transform.position, sortedTargets[j].transform.position) < Vector3.Distance(transform.position, sortedTargets[i].transform.position))
                {
                    GameObject temp = sortedTargets[i];
                    sortedTargets[i] = sortedTargets[j];
                    sortedTargets[j] = temp;
                }
            }
        }
    }
}