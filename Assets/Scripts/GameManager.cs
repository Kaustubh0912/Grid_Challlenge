using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform gridParent;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Button startButton;
    [SerializeField] private int gridSize = 20;
    [SerializeField] private float gameTime = 60f;
    [SerializeField] private float penaltyPercentage = 5f;
    [SerializeField] private float shuffleInterval = 2f;

    private List<int> numbers;
    private int currentNumber = 1;
    private float timeLeft;
    private bool isGameActive;
    private HashSet<int> correctlyClickedNumbers;
    private Cell lastWrongCell;
    private float nextShuffleTime;
    private List<Vector3> cellPositions;


    void Start()
    {
        // Verify required components
        if (cellPrefab == null)
        {
            Debug.LogError("Cell Prefab is not assigned!");
            return;
        }
        if (gridParent == null)
        {
            Debug.LogError("Grid Parent is not assigned!");
            return;
        }
        if (progressBar == null)
        {
            Debug.LogError("Progress Bar is not assigned!");
            return;
        }
        if (startButton == null)
        {
            Debug.LogError("Start Button is not assigned!");
            return;
        }

        correctlyClickedNumbers = new HashSet<int>();
        startButton.onClick.AddListener(StartGame);
        InitializeNumbers();
    }

    void StartGame()
    {
        if (numbers == null || numbers.Count == 0)
        {
            Debug.LogError("Numbers list is not initialized!");
            return;
        }

        currentNumber = 1;
        timeLeft = gameTime;
        correctlyClickedNumbers.Clear();
        lastWrongCell = null;
        isGameActive = true;
        CreateGrid();
        StoreCellPositions();
    }

    void StoreCellPositions()
    {
        cellPositions = new List<Vector3>();
        foreach (Transform child in gridParent)
        {
            cellPositions.Add(child.position);
        }
    }
    void ShuffleCellPositions()
    {
        if (!isGameActive) return;

        Cell[] cells = gridParent.GetComponentsInChildren<Cell>();
        List<Vector3> positions = new List<Vector3>(cellPositions);

        // Shuffle positions
        for (int i = positions.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Vector3 temp = positions[i];
            positions[i] = positions[randomIndex];
            positions[randomIndex] = temp;
        }

        // Apply shuffled positions
        if(timeLeft<gameTime-shuffleInterval)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                StartCoroutine(MoveCellToPosition(cells[i].transform, positions[i]));
            }
        }
    }
    IEnumerator MoveCellToPosition(Transform cellTransform, Vector3 targetPosition)
    {
        float duration = 0.2f; // Duration of movement animation
        float elapsed = 0;
        Vector3 startPosition = cellTransform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            t = t * t * (3f - 2f * t);

            cellTransform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        cellTransform.position = targetPosition;
    }
    void CreateGrid()
    {
        if (gridParent == null || cellPrefab == null)
        {
            Debug.LogError("Required references are missing!");
            return;
        }

        // Clear existing grid
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        ShuffleList(numbers);

        for (int i = 0; i < numbers.Count; i++)
        {
            GameObject cell = Instantiate(cellPrefab, gridParent);
            if (cell == null)
            {
                Debug.LogError($"Failed to instantiate cell {i}");
                continue;
            }

            Cell cellBtn = cell.GetComponent<Cell>();
            if (cellBtn == null)
            {
                Debug.LogError($"Cell component missing on instantiated object {i}");
                continue;
            }

            cellBtn.Setup(numbers[i], HandleCellClick);

            if (correctlyClickedNumbers.Contains(numbers[i]))
            {
                cellBtn.SetCorrect();
            }
        }
        StartCoroutine(StorePositionsNextFrame());

    }
    IEnumerator StorePositionsNextFrame()
    {
        yield return null; // Wait one frame
        StoreCellPositions();
    }

    void InitializeNumbers()
    {
        numbers = new List<int>();
        for (int i = 1; i <= gridSize; i++)
            numbers.Add(i);
    }

    

    void HandleCellClick(int number) 
    {
        if (!isGameActive) return;

        if(lastWrongCell != null)
        {
            lastWrongCell.ResetVisual();
            lastWrongCell = null;
        }

        Cell clickedCell = FindCellWithNumber(number);

        if(number==currentNumber)
        {
            correctlyClickedNumbers.Add(number);
            if (clickedCell != null)
                clickedCell.SetCorrect();
            currentNumber++;
            CheckWinCondition();
        }
        else 
        {
            if (clickedCell != null && !correctlyClickedNumbers.Contains(clickedCell.GetNumber()))
            {
                clickedCell.SetWrong();
                lastWrongCell = clickedCell;
            }

            ApplyTimePenalty();
        }
    }


    private Cell FindCellWithNumber(int number)
    {
        foreach(Transform child in gridParent)
        {
            Cell cell = child.GetComponent<Cell>();
            if (cell != null && cell.GetNumber() == number)
                return cell;
        }

        return null;
    }
    void ApplyTimePenalty()
    {
        float penalty = timeLeft * (penaltyPercentage / 100.0f);
        timeLeft = Mathf.Max(0, timeLeft - penalty);
    }
    void CheckWinCondition() 
    {
        if (currentNumber > gridSize)
        {
            isGameActive = false;
            Debug.Log("You Win!");
        }

    }


    private void Update()
    {
        if(isGameActive)
        {
            timeLeft = Mathf.Max(0, timeLeft - Time.deltaTime);
            progressBar.value = timeLeft / gameTime;

            if (Time.time >= nextShuffleTime)
            {
                ShuffleCellPositions();
                nextShuffleTime = Time.time + shuffleInterval;
            }

            if (timeLeft<=0)
            {
                isGameActive = false;
                Debug.Log("Time's Up! You Lose!");
            }
        }
    }

    void ShuffleList<A>(List<A> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;    
            int k = Random.Range(0, n + 1);
            A value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public float ShuffleInterval
    {
        get => shuffleInterval;
        set => shuffleInterval = value;
    }

    public float PenaltyPercentage
    {
        get => penaltyPercentage;
        set => penaltyPercentage = value;
    }

    public float GameTime
    {
        get => gameTime;
        set => gameTime = value;
    }

 }
  

