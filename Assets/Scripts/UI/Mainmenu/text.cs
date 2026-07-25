using UnityEngine;
using UnityEngine.EventSystems;

public class text : MonoBehaviour, 
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Vector3 n = Vector3.one;
    public Vector3 h = new Vector3(1.1f, 1.1f, 1f);
    public Vector3 c = new Vector3(0.95f, 0.95f, 1f);

    void Start()
    {

        transform.localScale = n;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = h;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = n;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = c;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = h;
    }
}