using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OpenPackage : MonoBehaviour
{
    // Start is called before the first frame update


    //Primer za dotween premikanje - pads.transform.DOLocalMove(new Vector3(-0.418f, 3.29f, 2.839f), 1f);
    public GameObject packageAndCover;
    public GameObject package;
    public GameObject TopPackage;
    public GameObject fakeCover;
    public GameObject originalCover;
    public Animator packageAnim;
    public Animator coverAnim;

    bool tearAreaClicked = false;

    public string startTearArea = "TearArea";
    private Vector3 startPosition;
    Vector3 offset;



    private void OnMouseUp()
    {
        tearAreaClicked = false;
    }

    private void Start()
    {
      
    }

    public void DetectObjectWithRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.CompareTag(startTearArea))
                {
                    tearAreaClicked = true;
                }
            }
        }
            

    }

    private void Update()
    {
        DetectObjectWithRaycast();
    }


    void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameManagerOnStateChanged;
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    private void GameManagerOnStateChanged(GameState state)
    {
        if (state == GameState.OpenPackage)
        {
            StartCoroutine(PackageCoverFadeIn());

        }


    }
    IEnumerator PackageCoverFadeIn()
    {
        yield return new WaitForSeconds(2f);
        LeanTween.moveLocal(packageAndCover, new Vector3(0f, 0f, 0f), 1.5f).setEaseInOutExpo();
    }

    IEnumerator PackageFadeOut()
    {
        yield return new WaitForSeconds(1f);
        LeanTween.moveLocal(package, new Vector3(-0.543f, 0f, 0.385f), 1.5f).setEaseInOutExpo();
     
        
    }

    void RemoveTopPackage()
    {
        LeanTween.moveLocal(TopPackage, new Vector3(0.28f, 1.589f, -22.884f), 1.5f);
    }

    IEnumerator MoveCover() { 


        coverAnim.SetBool("playCover", true);
        
        yield return new WaitForSeconds(0.13f);
        LeanTween.moveLocal(fakeCover, new Vector3(0.35f, 1.377f, -22.905f), 1.5f).setEaseInOutExpo();
        yield return new WaitForSeconds(1.4f);
        fakeCover.SetActive(false);
        originalCover.SetActive(true);

    }

    IEnumerator BeginOpening()
    {
        packageAnim.SetBool("packageOpen", true);
        FindObjectOfType<AudioManager>().Play("PaperTear");
        StartCoroutine(PackageFadeOut());
        yield return new WaitForSeconds(0.5f);
        RemoveTopPackage();
        yield return new WaitForSeconds(1.2f);
        StartCoroutine(MoveCover());



    }
    public void SwipeRight()
    {
        if (tearAreaClicked) StartCoroutine(BeginOpening());
    }
}
