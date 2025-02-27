using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private float elixirCost;
    public float increaseElixirby = 0.5f;
    public Vector3 startPosition;
    private Camera mainCamera;
    private Slider elixirSlider;
    private TextMeshProUGUI elixirText;
    private bool isDraggable = false;
    public bool isPlaced = false;
    private Transform originalParent;

    public TextMeshProUGUI ExilerCost; // Reference to TextMeshPro UI element

    private void Awake()
    {
        mainCamera = Camera.main;
        elixirSlider = GameObject.Find("ElixirSlider").GetComponent<Slider>();
        elixirText = GameObject.Find("ElixirText").GetComponent<TextMeshProUGUI>();

        //// Ensure a Collider2D is attached
        //if (GetComponent<Collider2D>() == null)
        //{
        //    gameObject.AddComponent<BoxCollider2D>();
        //}
    }

    private void Start()
    {
        elixirCost = GetComponent<Card>().elixercost;
        startPosition = transform.position;
        originalParent = transform.parent;
        //InvokeRepeating("IncreaseElixir", 1f, 1f);
        //UpdateElixirText();
    }

    public void Update()
    {
        IncreaseElixir();
        UpdateExilerCostText();
    }

    void IncreaseElixir()
    {
        if (elixirSlider.value < elixirSlider.maxValue)
        {
            elixirSlider.value += increaseElixirby * Time.deltaTime;
            UpdateElixirText();
        }
    }

    void UpdateElixirText()
    {
        if (elixirText != null)
        {
            elixirText.text = Mathf.Floor(elixirSlider.value).ToString();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (elixirSlider.value >= elixirCost && !isPlaced)
        {
            isDraggable = true;
            startPosition = transform.position;

            // Detach from parent to allow free movement
            transform.SetParent(null);
        }
        else
        {
            isDraggable = false;
            eventData.pointerDrag = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Mathf.Abs(mainCamera.transform.position.z); // Use the camera's distance

            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDraggable)
        {
            isPlaced = true;
            elixirSlider.value -= elixirCost;
            UpdateElixirText();

        }
    }

    void UpdateExilerCostText()
    {
        if (ExilerCost != null)
        {
            ExilerCost.text =  elixirCost.ToString("0") ;
        }
    }
}