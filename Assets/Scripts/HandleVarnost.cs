using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HandleVarnost : MonoBehaviour
{
    public float delayTime;
    public GameObject button;
    private Vector3 temp = new Vector3(-2.3614f, 1.625f, -22.231f);
    public VisualEffect smokeVfx;
    [SerializeField]
    DialogTrigger correctAnswer;

    public Camera HanzCamera;
    [SerializeField] private GameObject polijVoda, pokrijPokrov, gasilniAparat;

    bool pokrovClicked = false;
    public DataManager dataManager;


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

        polijVoda.SetActive(state == GameState.Varnost);
        pokrijPokrov.SetActive(state == GameState.Varnost);
        gasilniAparat.SetActive(state == GameState.Varnost);

    }

    public IEnumerator HandleButton()
    {
        FindObjectOfType<AudioManager>().Play("Correct");
        correctAnswer.TriggerDialog();
        yield return new WaitForSeconds(2f);
        ChangeCamera.instance.ChangeToCamera(HanzCamera);
        //STATE Varnost je sedaj koncana, NOV STATE
        GameManager.instance.UpdateGameState(GameState.VarnostKoncano);
    }

    public void HandleButtonOff(){
        StartCoroutine(HandleButton());
        smokeVfx.Stop();
    }

    public void IncorrectSFX()
    {
        FindObjectOfType<AudioManager>().Play("Incorrect");
        VPManager.instance.Decrease();
    }
}