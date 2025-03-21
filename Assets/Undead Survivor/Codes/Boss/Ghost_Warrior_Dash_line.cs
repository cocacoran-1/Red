using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Warrior_Dash_line : MonoBehaviour
{
    public LineRenderer lineRenderer;

    Player player;
    public bool isStart = true;

    Vector3 direction;
    Vector3 endPoint;
    float distance;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }
    private void OnEnable()
    {
        isStart = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isLive)
        {
            return;
        }
        direction = (player.transform.position - transform.position).normalized;
        distance = Vector3.Distance(player.transform.position, transform.position) + 2f;
        endPoint = transform.position + direction * distance;
        lineRenderer.SetPosition(0, transform.position);
        if (isStart)
        {
            lineRenderer.SetPosition(1, endPoint);
        }
    }
}
