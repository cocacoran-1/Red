using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning_Chain : MonoBehaviour
{
    public float damage = 10f;
    public float chainDelay = 0.1f;

    private LineRenderer lineRenderer;
    public float fpsCounter;
    [SerializeField]
    private Texture[] textures;
    public int animationStep;
    public float fps = 30f;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        fpsCounter += Time.deltaTime;
        if (fpsCounter >= 1f / fps)
        {
            animationStep++;
            if (animationStep == textures.Length)
            {
                animationStep = 0;
                gameObject.SetActive(false);
            }
            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);
            fpsCounter = 0f;
        }
    }

    public IEnumerator Connect(Transform origin, Transform target)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, target.position);

        if (target.gameObject.activeSelf) // 애니메이션 시작 시점에 데미지 처리
        {
            target.GetComponent<Enemy>().onDamaged(damage);
        }

        yield return new WaitForSeconds(chainDelay);
    }
}