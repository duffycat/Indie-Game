using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public delegate void MenuButtonAction();

    [Tooltip("defaut")]
    [SerializeField] private Color defultColor;
    [Tooltip("defaut selected")]
    [SerializeField] private Color selectedColor;
    [Tooltip("defaut hover")]
    [SerializeField] private Color highlightColor;
    [SerializeField] private UnityEvent onActivate;

    private bool mouseOver = false;
    private Image image;
    private MenuNav instance;


    public event MenuButtonAction ActivateEvent = delegate { };
    public event MenuButtonAction SelectEvent = delegate { };

    private void Awake()
    {
        TryGetComponent(out image);
        transform.parent.TryGetComponent(out instance);

    }

    void Start()
    {
        image.color = defultColor;
    }

    void Update()
    {
        if(mouseOver == true && Input.GetButtonDown("Fire1") == true) 
        {
            if (instance.SelectedButton == this)
            {
                Activate();
            }
            else
            {
                Select();
            }
        }
    }

    public void Select()
    {
        SelectEvent.Invoke();
    }

    public void Activate()
    {
        ActivateEvent.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        if(instance.SelectedButton != this)
        {
            image.color = highlightColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        if(image.color == highlightColor && this != instance.SelectedButton) 
        {
            image.color = defultColor;
        }
    }

    private void OnActivate() 
    { 
        onActivate.Invoke();
    }

    private void OnSelcet() 
    { 
        if(instance.SelectedButton != null) 
        {
            instance.SelectedButton.image.color = instance.SelectedButton.defultColor;
        }
        instance.SelectedButton = this;
        image.color = selectedColor;
    }

    private void OnEnable()
    {
        ActivateEvent += OnActivate;
        SelectEvent += OnSelcet;
    }

    private void OnDisable()
    {
        ActivateEvent -= OnActivate;
        SelectEvent -= OnSelcet;
    }
}
