using System.Collections;
using UnityEngine;

public class PlayerUi : MonoBehaviour 
{
    private static readonly int FullHash = Animator.StringToHash("Full");
    private static WaitForSeconds _waitForSeconds0_02 = new(0.02f);

    [SerializeField] GameObject[] Hearts;
    [SerializeField] private GameObject _cdImage;
    private UnityEngine.UI.Image _cdFill;

    private PlayerControls _controls;
    public static PlayerUi Instance;

    private IEnumerator ShowFireCd()
    {
        float duration = _controls.FireCd;
        float elapsed = 0f;

        _cdFill.fillAmount = 1f;

        while (elapsed < duration)
        {
            yield return null;

            elapsed += Time.unscaledDeltaTime;

            _cdFill.fillAmount = Mathf.Lerp(1f, 0f, elapsed / duration);
        }

        _cdFill.fillAmount = 0f;
    }


    void HealthControl()
    {
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (_controls.Health - 1 >= i && Hearts[i] != null)
            {
                Hearts[i].GetComponent<Animator>().SetBool(FullHash, true);
            }
            else if (_controls.Health - 1 < i && Hearts[i] != null)
            {
                Hearts[i].GetComponent<Animator>().SetBool(FullHash, false);
            }
        }
    }

    public void StartCD()
    {
        _cdFill.fillAmount = 1;
        StartCoroutine(ShowFireCd());
    }

    private void Awake()
    {
        _controls = GetComponent<PlayerControls>();
    }

    private void Start()
    {
        _controls.Health = 3;
        Instance = this;
        _cdFill = _cdImage.GetComponent<UnityEngine.UI.Image>();
        _cdFill.fillAmount = 0;
    }

    private void Update()
    {
         HealthControl();
    }
}
