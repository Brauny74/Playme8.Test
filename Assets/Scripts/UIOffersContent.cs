using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Этот класс работает с окном предложений.
/// </summary>
public class UIOffersContent : MonoBehaviour
{
    [Tooltip("Префаб для предложения. Должен содержать компонент UIOffer")]
    public UIOffer UIOfferPrefab;
    [Tooltip("Transform, куда будут добавляться новые предложения.")]
    public RectTransform ContentTransform;
    [Tooltip("Позиция с которой начинается движение содержимого.")]
    public Vector2 StartOffersPosition;
    [Tooltip("Это значение на которое они будут сдвигаться. В данном проекте отсюда берётся только х.")]
    public Vector2 StepVector;
    [Tooltip("Скорость сдвига при нажатии кнопок.")]
    public float MovingSpeed = 500.0f;
        
    private Vector2 startContentPos;
    private Vector2 finishContentPos;

    private Vector2 targetPos;
    private bool isMoving = false;
    private bool isButtonPressed;

    private void Start()
    {
        startContentPos = ContentTransform.anchoredPosition;//получаем самую левую (стартовую) позицию контента\
        isButtonPressed = false;
    }

    /// <summary>
    /// Эта функция отображает предложения в окне.
    /// Она отображает их в сетке по три ряда и сколько нужно столбцов.
    /// </summary>
    /// <param name="offers">список с предложениями</param>
    public void Init(List<Offer> offers)
    {
        Vector2 coords = Vector2.zero; //координаты объект в сетке
        foreach (Offer offer in offers)
        {
            UIOffer newUIOffer = Instantiate<UIOffer>(UIOfferPrefab, ContentTransform);
            newUIOffer.Init(offer);
            newUIOffer.rectTransform.anchoredPosition = StartOffersPosition + coords * StepVector;
            coords = new Vector2(coords.x, coords.y + 1);
            if (coords.y > 2.0f)
            {
                coords = new Vector2(coords.x + 1, 0);
                ContentTransform.sizeDelta = new Vector2(ContentTransform.sizeDelta.x + StepVector.x, ContentTransform.sizeDelta.y);                
            }            
        }
        finishContentPos = new Vector2((ContentTransform.sizeDelta.x - StepVector.x) * -1, ContentTransform.sizeDelta.y);//получаем самую правую позицию контента
    }

    private void Update()
    {
        if (isMoving)//двигаем контент, если игрок нажал кнопку и мы ещё не дошли до нужной позиции
        {
            if (Mathf.Abs(targetPos.x - ContentTransform.anchoredPosition.x) < 0.001f)
            {
                isMoving = false;
                return;
            }
            ContentTransform.anchoredPosition = Vector2.MoveTowards(ContentTransform.anchoredPosition, targetPos, MovingSpeed * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        if (isButtonPressed)
        {
            if (Input.touchCount <= 0 || Input.GetMouseButtonUp(0))
            {
                isButtonPressed = false;
            }
        } else 
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                isMoving = false;
            }
        }
    }

    /// <summary>
    /// Эта функция сдвигает контент и все предложения на значение шага.
    /// </summary>
    /// <param name="direction">Направление сдвига. -1 - вправо, 1 - влево.</param>
    public void MoveContent(float direction)
    {
        float oldX = ContentTransform.anchoredPosition.x;//изначальная позиция контента
        float newX = ContentTransform.anchoredPosition.x + StepVector.x * direction;//позиция куда нужно сдвинуть экран по нажатию экрана
        newX = newX > startContentPos.x ? startContentPos.x : newX;
        newX = newX < finishContentPos.x ? finishContentPos.x : newX;
        targetPos = new Vector2(newX, ContentTransform.anchoredPosition.y);
        isMoving = true;
        isButtonPressed = true;
    }
}
