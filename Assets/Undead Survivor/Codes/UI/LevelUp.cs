using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelUp : MonoBehaviour
{
    private SkillLevel skillLevel;
    SkillLevel[] skills;
    GameManager gameManager;
    CanvasManager canvasManager;
    public GameObject AppearSkills;  // 나타나는 애니메이션을 가진 객체
    public Animator AppearAnim;
    public GameObject uiLevelUp; // 스킬 선택 창

    [Header("스탬프")]
    public Button[] buttons;
    public GameObject stampObject; // 스탬프 객체
    public Animator StampAnimator; // 스탬프 애니메이터
    public GameObject StampParent;
    public GameObject MaskParent;
    public GameObject Mask;

    public Canvas canvas;
    public RectTransform stampRectTransform;
    public RectTransform maskRectTransform;

    public float offsetX = 100f;
    public float offsetY = 450f;

    public List<Button> ActiveButtons;
    public List<Button> PassiveButtons;
    public List<Button> ItemButtons;

    public List<Button> SelectActive = new List<Button>();
    public List<Button> SelectPassive = new List<Button>();

    private Dictionary<Button, int> ButtonPressCount = new Dictionary<Button, int>();

    private void Awake()
    {
        skills = GetComponentsInChildren<SkillLevel>(true);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        canvasManager = GetComponent<CanvasManager>();
    }
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => OnButtonClick(buttons[buttonIndex]));
        }
    }
    private void Update()
    {
        if (AppearSkills != null)
        {
            if (AppearSkills.activeSelf && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                AppearAnim.SetTrigger("Appear");
            }
        }
    }
    public void Show()
    {
        uiLevelUp.SetActive(true);
        StartCoroutine(Next());
        gameManager.Stop();
    }

    public void Hide()
    {
        gameManager.Resume();
        uiLevelUp.SetActive(false);
    }

    public void Select(int index)
    {
        skills[index].OnClick();
    }

    IEnumerator Next()
    {
        AppearSkills.SetActive(true);

        // 모든 버튼을 비활성화
        foreach (Button button in ActiveButtons)
        {
            button.gameObject.SetActive(false);
        }
        foreach (Button button in PassiveButtons)
        {
            button.gameObject.SetActive(false);
        }

        // 랜덤으로 뽑아올 버튼들 리스트
        List<Button> availableButtons = new List<Button>();

        // 선택한 액티브, 패시브 스킬들이 각각 6개 보다 적을 때
        if (SelectActive.Count < 6 && SelectPassive.Count < 6)
        {
            availableButtons.AddRange(ActiveButtons);
            availableButtons.AddRange(PassiveButtons);
        }
        // 선택한 액티브 스킬이 6개, 패시브 스킬이 6개 보다 적을 때
        else if (SelectActive.Count == 6 && SelectPassive.Count < 6)
        {
            availableButtons.AddRange(SelectActive);
            availableButtons.AddRange(PassiveButtons);
        }
        // 선택한 액티브 스킬이 6개 보다 적고 패시브 스킬이 6개 일 때
        else if (SelectActive.Count < 6 && SelectPassive.Count == 6)
        {
            availableButtons.AddRange(ActiveButtons);
            availableButtons.AddRange(SelectPassive);
        }
        // 선택한 액티브 스킬이 6개 , 패시브 스킬이 6개 일 때
        else if (SelectActive.Count == 6 && SelectPassive.Count == 6)
        {
            // 선택해서 리스트에 추가된 스킬들은 최대 6번 클릭 가능 (최대 7레벨)
            availableButtons.AddRange(SelectActive.FindAll(b => ButtonPressCount[b] < 7));
            availableButtons.AddRange(SelectPassive.FindAll(b => ButtonPressCount[b] < 7));
        }

        // 만랩인 스킬 제거
        availableButtons.RemoveAll(b => ButtonPressCount.ContainsKey(b) && ButtonPressCount[b] >= 7);

        // 모든 패시브,액티브가 만랩일 때, 아이템 버튼만 나오도록 리스트 변경
        if (availableButtons.Count == 0)
        {
            availableButtons.AddRange(ItemButtons);
        }

        // 리스트에 추가된 버튼들 중에서 랜덤으로 3개 선택
        for (int i = 0; i < 3; i++)
        {
            Button randomButton = availableButtons[Random.Range(0, availableButtons.Count)];
            availableButtons.Remove(randomButton);
            randomButton.gameObject.SetActive(true);
        }


        // 스킬 선택 UI가 활성화된 상태인 동안 대기
        yield return new WaitUntil(() => uiLevelUp == null || uiLevelUp.activeSelf == false);

        // uiLevlelUp 객체가 사라지면 도장, 문양을 비활성화
        if (StampParent != null)
        {
            StampParent.SetActive(false);
        }

        if (MaskParent != null)
        {
            MaskParent.SetActive(false);
        }

    }
    void OnButtonClick(Button clickedButton)
    {
        if (ActiveButtons.Contains(clickedButton))
        {
            if (!SelectActive.Contains(clickedButton))
            {
                SelectActive.Add(clickedButton);
            }
        }
        else if (PassiveButtons.Contains(clickedButton))
        {
            if (!SelectPassive.Contains(clickedButton))
            {
                SelectPassive.Add(clickedButton);
            }
        }

        if (!ButtonPressCount.ContainsKey(clickedButton))
        {
            ButtonPressCount[clickedButton] = 0;
        }
        ButtonPressCount[clickedButton]++;

        // 버튼을 클릭 시 버튼 위치 값 가져오기
        RectTransform buttonRectTransform = clickedButton.GetComponent<RectTransform>();
        Vector3 worldPosition = buttonRectTransform.TransformPoint(buttonRectTransform.rect.center);

        StampParent.SetActive(true);
        MaskParent.SetActive(true);

        MoveAndActivate(worldPosition);
    }

    public IEnumerator StartSkill()
    {
        AppearSkills.SetActive(true);

        // 모든 버튼을 비활성화
        foreach (Button button in ActiveButtons)
        {
            button.gameObject.SetActive(false);
        }
        foreach (Button button in PassiveButtons)
        {
            button.gameObject.SetActive(false);
        }

        // 무작위로 선택될 버튼의 리스트
        List<Button> availableButtons = new List<Button>(ActiveButtons);

        // ActiveButtons 중에서 무작위로 3개의 버튼을 선택
        for (int i = 0; i < 3; i++)
        {
            if (availableButtons.Count > 0)
            {
                Button randomButton = availableButtons[Random.Range(0, availableButtons.Count)];
                availableButtons.Remove(randomButton);
                randomButton.gameObject.SetActive(true);
            }
        }

        // 스킬 선택 UI가 활성화된 동안 대기
        yield return new WaitUntil(() => uiLevelUp == null || uiLevelUp.activeSelf == false);

        // uiLevelUp 객체가 사라지면 스탬프와 패턴을 비활성화
        if (StampParent != null)
        {
            StampParent.SetActive(false);
        }

        if (MaskParent != null)
        {
            MaskParent.SetActive(false);
        }
    }


    // 도장, 문양의 위치를 버튼 위치로 조정
    void MoveAndActivate(Vector3 worldPosition)
    {
        worldPosition.x += offsetX;
        worldPosition.y += offsetY;

        stampRectTransform.position = worldPosition;
        maskRectTransform.position = worldPosition;
        stampRectTransform.gameObject.SetActive(true);
    }
}