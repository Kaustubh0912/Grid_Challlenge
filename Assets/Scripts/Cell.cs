
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;


public class Cell : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private Image bgImage;
    private Button button;

    private int number;
    private Action<int> onClick;
    private bool isWrong=false;
    private bool isCorrect=false;


    private void Awake()
    {
        // Get the Button component and add listener
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnButtonClick());
        }
    }
    public void Setup(int number, Action<int> onClick)
    {
        this.number = number;
        this.onClick = onClick;
        numberText.text = number.ToString();
        ResetVisual();
    }

    public void OnButtonClick()
    {
        Debug.Log($"Clicked number: {number}"); // Debug log
        onClick?.Invoke(number);
    }

    public void SetCorrect()
    {
        bgImage.color = new Color(0, 1, 0, 0.35f);
        numberText.color = Color.green;
        isWrong = false;
        isCorrect = true;
    }

    public void SetWrong()
    {
        isWrong = true;
        numberText.color = Color.red;
    }

    public void ResetVisual()
    {
        if(!isCorrect)
        {
            isWrong = false;
            bgImage.color = new Color(255f, 255f, 255f, 1f);
            numberText.color = Color.black;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isWrong)
        {
            numberText.color = Color.red;
        }
    }

    // Called when pointer is released
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isWrong)
        {
            numberText.color = Color.white;
        }
    }

    public int GetNumber()
    {
        return number;
    }
}
