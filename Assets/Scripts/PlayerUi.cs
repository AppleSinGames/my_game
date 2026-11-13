using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerControls))]
public class PlayerUi : MonoBehaviour 
{
    //----------------------------HASH----------------------------
    private static readonly int FullHash = Animator.StringToHash("Full");
    private static WaitForSeconds WitForSeconds0_02 { get; } = new(0.02f);

    //----------------------------UNITY----------------------------
    [SerializeField] GameObject[] Hearts;
    [SerializeField] private UnityEngine.UI.Image _fireballCD;
    [SerializeField] private UnityEngine.UI.Image _dashCD;

    private PlayerControls PlayerScript;
    public static PlayerUi Instance;
    private Spell _fireball;
    private Spell _dash;
    private Unit _player;

    public void HealthUpdate()
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
            Debug.Log(_player.Health);
        }
    }

    private static IEnumerator ShowCd(Image fill, float cooldown)
    {
        float elapsed = 0f;

        fill.fillAmount = 1f;

        while (elapsed < cooldown)
        {
            yield return null;
            elapsed += Time.deltaTime;
            fill.fillAmount = Mathf.Lerp(1f, 0f, elapsed / cooldown);
        }
        fill.fillAmount = 0f;
    }

    public void Fireball()
    {
        StartCoroutine(ShowCd(_fireballCD, _fireball.Cooldown));
    }
    public void Dash()
    {
        StartCoroutine(ShowCd(_dashCD, _dash.Cooldown));
    }

    private void Awake()
    {
        PlayerScript = GetComponent<PlayerControls>();
        _fireball = PlayerScript.Fireball;
        _dash = PlayerScript.Dash;
        _player = PlayerScript.Player;
    }

    private void Start()
    {
        Instance = this;
        _fireballCD.fillAmount = 0;
        _dashCD.fillAmount = 0;
    }
}
