using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll_Dash_line : MonoBehaviour
{
    public LineRenderer lineRenderer;
    Boss_Troll Boss_Troll;
    GameObject Boss_point;
    Player player;
    Vector3 direction;
    Vector3 endPoint;
    float distance;
    public bool isStart = true;
    public Transform player_pos;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Boss_point = GameObject.Find("Start_point");
        player = GameObject.Find("Player").GetComponent<Player>();
        Boss_Troll = GetComponentInParent<Boss_Troll>();
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
        direction = (player.transform.position - Boss_point.transform.position).normalized;
        distance = Vector3.Distance(player.transform.position, Boss_point.transform.position) + 2f;
        endPoint = Boss_point.transform.position + direction * distance;
        lineRenderer.SetPosition(0, Boss_point.transform.position);
        if (isStart)
        {

            lineRenderer.SetPosition(1, endPoint);
        }

    }


}
