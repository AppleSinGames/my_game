using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerControls))]
public class PlayerUi : MonoBehaviour 
{
    //----------------------------HASH----------------------------
    private static readonly int FullHash = Animator.StringToHash("Full");
    private static WaitForSeconds WitForSeconds0_02 { get; } = new(0.02f);

    //----------------------------UNITY----------------------------
    [SerializeField] GameObject[] Hearts;
    [SerializeField] private GameObject _cdImage;
    private UnityEngine.UI.Image _cdFill;

    private PlayerControls PlayerScript;
    public static PlayerUi Instance;
    private Spell _fireball;
    private Unit _player;

    void HealthControl()
    {
        for (int i = 0; i < Hearts.Length; i++)
        {
            if (_player.Health - 1 >= i && Hearts[i] != null)
            {
                Hearts[i].GetComponent<Animator>().SetBool(FullHash, true);
            }
            else if (_player.Health - 1 < i && Hearts[i] != null)
            {
                Hearts[i].GetComponent<Animator>().SetBool(FullHash, false);
            }
        }
    }

    private IEnumerator ShowFireCd()
    {
        float duration = _fireball.Cooldown;
        float elapsed = 0f;

        _cdFill.fillAmount = 1f;

        while (elapsed < duration)
        {
            yield return null;
            elapsed += Time.deltaTime;
            _cdFill.fillAmount = Mathf.Lerp(1f, 0f, elapsed / duration);
        }
        _cdFill.fillAmount = 0f;
    }

    public void StartCD()
    {
        _cdFill.fillAmount = 1;
        StartCoroutine(ShowFireCd());
    }

    private void Awake()
    {
        PlayerScript = GetComponent<PlayerControls>();
        _fireball = PlayerScript.Fireball;
        _player = PlayerScript.Player;
    }

    private void Start()
    {
        Instance = this;
        _cdFill = _cdImage.GetComponent<UnityEngine.UI.Image>();
        _cdFill.fillAmount = 0;
    }

    private void Update()
    {
         HealthControl();
    }
}
