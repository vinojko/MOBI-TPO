using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropQuiz : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Vector2 lastMousePosition;
    private Vector3 offset;
    private Vector3 startPosition;
    private bool draggable = true;

    public string destinationTag;

    /// <summary>
    /// This method will be called on the start of the mouse drag
    /// </summary>
    /// <param name="eventData">mouse pointer event data</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        lastMousePosition = eventData.position;
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }

    void Start()
    {
        startPosition = transform.position;
    }

    /// <summary>
    /// This method will be called during the mouse drag
    /// </summary>
    /// <param name="eventData">mouse pointer event data</param>
    public void OnDrag(PointerEventData eventData)
    {
        if(draggable)
        transform.position = Input.mousePosition + offset;
    }

    /// <summary>
    /// This method will be called at the end of mouse drag
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        if (draggable)
        {
            Debug.Log("End Drag");
            RaycastResult raycastResult = eventData.pointerCurrentRaycast;
            Debug.Log(raycastResult.gameObject.tag);

            if (raycastResult.gameObject.CompareTag(destinationTag))
            {
                Debug.Log("Touched");
                transform.position = raycastResult.gameObject.transform.position;
                raycastResult.gameObject.SetActive(false);
                draggable = false;

                switch (destinationTag)
                {
                    case "QuestionVarnost":
                        QuizResults.instance.varnost = true;
                        StartCoroutine(QuizResults.instance.End());
                        FindObjectOfType<AudioManager>().Play("Correct");
                        break;
                    case "QuestionOdzivnost":
                        QuizResults.instance.odzivnost = true;
                        StartCoroutine(QuizResults.instance.End());
                        FindObjectOfType<AudioManager>().Play("Correct");
                        break;
                    case "QuestionDihanje":
                        QuizResults.instance.dihanje = true;
                        StartCoroutine(QuizResults.instance.End());
                        FindObjectOfType<AudioManager>().Play("Correct");
                        break;
                    case "QuestionCPR":
                        QuizResults.instance.kpo = true;
                        StartCoroutine(QuizResults.instance.End());
                        FindObjectOfType<AudioManager>().Play("Correct");
                        break;
                    case "QuestionAED":
                        QuizResults.instance.aed = true;
                        StartCoroutine(QuizResults.instance.End());
                        FindObjectOfType<AudioManager>().Play("Correct");
                        break;
                }
            }
            else
            {
                transform.position = startPosition;
                FindObjectOfType<AudioManager>().Play("Incorrect");
            }
        }
    
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("pinter down");
        offset = transform.position - Input.mousePosition;
    }

    /// <summary>
    /// This methods will check is the rect transform is inside the screen or not
    /// </summary>
    /// <param name="rectTransform">Rect Trasform</param>
    /// <returns></returns>
    private bool IsRectTransformInsideSreen(RectTransform rectTransform)
    {
        bool isInside = false;
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        int visibleCorners = 0;
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);
        foreach (Vector3 corner in corners)
        {
            if (rect.Contains(corner))
            {
                visibleCorners++;
            }
        }
        if (visibleCorners == 4)
        {
            isInside = true;
        }
        return isInside;
    }
}