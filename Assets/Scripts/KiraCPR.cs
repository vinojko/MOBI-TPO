using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiraCPR : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera kiraCam, AEDCam;
    public Animator kiraAnimator;
    int kiraHash;

    public DialogTrigger kiraComes, aedTakenDialog;
    public GameObject AED, AEDIcon, groundAED;

    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;

    }
    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    private void GameManagerOnStateChanged(GameState state)
    {
        if (state == GameState.CPRKira)
        {
            kiraComes.TriggerDialog();
            StartRunning();
            ChangeCamera.instance.ChangeToCameraSlow(kiraCam);
            StartCoroutine(ShowAEDIcon());
        }
    }
    void Start()
    {
        kiraHash = Animator.StringToHash("Kira");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartRunning()
    {

        kiraAnimator.SetBool("StartRunning", true);
        /*LeanTween.value(gameObject, 1f, 2f, 0.5f).setOnUpdate((value) =>
        {
            kiraAnimator.SetFloat(kiraHash, value);
        });*/

        LeanTween.value(gameObject, 0f, 1f, 0.5f).setDelay(1.6f).setOnUpdate((value) =>
        {
            kiraAnimator.SetFloat(kiraHash, value);
        });
    }

    private IEnumerator ShowAEDIcon()
    {
        yield return new WaitForSeconds(2f);
        AEDIcon.SetActive(true);
    }

    private IEnumerator AEDKira()
    {
        AEDIcon.SetActive(false);
        AED.SetActive(false);
        groundAED.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        ChangeCamera.instance.ChangeToCameraSlow(AEDCam);
        yield return new WaitForSeconds(1f);
        aedTakenDialog.TriggerDialog();
        yield return new WaitForSeconds(2f);
        GameManager.instance.UpdateGameState(GameState.CPRKoncano);
    }

    public void AEDTaken()
    {
        StartCoroutine(AEDKira());
    }
}
