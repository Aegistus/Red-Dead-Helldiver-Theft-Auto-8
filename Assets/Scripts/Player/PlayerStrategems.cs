using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System;

public class PlayerStrategems : MonoBehaviour
{
    public event Action OnInputChange;
    public event Action OnEnterStratagemMode;
    public event Action OnExitStratagemMode;
    public event Action OnReadyToThrow;

    [SerializeField] Transform strategemBallSpawnPoint;
    [SerializeField] float throwForce = 100f;

    public List<Strategem> unlockedStrategems = new List<Strategem>();

    [HideInInspector] public List<DDR_Direction> currentInput = new List<DDR_Direction>();
    GameObject currentStrategemBall = null;

    bool inStrategemMode = false;
    bool readyToThrow = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            EnterStrategemMode();
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && currentStrategemBall == null)
        {
            ExitStrategemMode();
        }
        if (inStrategemMode)
        {
            if (readyToThrow)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    ThrowBall();
                    ExitStrategemMode();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    currentInput.Add(DDR_Direction.UP);
                    OnInputChange?.Invoke();
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    currentInput.Add(DDR_Direction.DOWN);
                    OnInputChange?.Invoke();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    currentInput.Add(DDR_Direction.RIGHT);
                    OnInputChange?.Invoke();
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    currentInput.Add(DDR_Direction.LEFT);
                    OnInputChange?.Invoke();
                }
                CheckForCompletedStrategem();
                PrintInput();
            }
        }
        if (currentStrategemBall != null)
        {
            currentStrategemBall.transform.localPosition = Vector3.zero;
        }
    }

    public void EnterStrategemMode()
    {
        inStrategemMode = true;
        currentInput = new List<DDR_Direction>();
        OnEnterStratagemMode?.Invoke();
    }

    public void ExitStrategemMode()
    {
        inStrategemMode = false;
        readyToThrow = false;
        if (currentStrategemBall != null)
        {
            Destroy(currentStrategemBall);
        }
        currentInput = new List<DDR_Direction>();
        OnInputChange?.Invoke();
        OnExitStratagemMode?.Invoke();
    }

    void ThrowBall()
    {
        currentStrategemBall.GetComponent<StrategemBall>().thrown = true;
        currentStrategemBall.transform.SetParent(null);
        Rigidbody ballRB = currentStrategemBall.GetComponent<Rigidbody>();
        ballRB.isKinematic = false;
        ballRB.useGravity = true;
        ballRB.AddForce(throwForce * strategemBallSpawnPoint.forward);
        currentStrategemBall = null;
    }

    void CheckForCompletedStrategem()
    {
        Strategem completed = null;
        for (int i = 0; i < unlockedStrategems.Count; i++)
        {
            if (CodesAreEqual(unlockedStrategems[i].ddrCode, currentInput))
            {
                print("COMPLETED CODE");
                completed = unlockedStrategems[i];
                break;
            }
        }
        if (completed != null)
        {
            currentStrategemBall = Instantiate(completed.strategemBallPrefab, strategemBallSpawnPoint.position, strategemBallSpawnPoint.rotation, strategemBallSpawnPoint);
            readyToThrow = true;
            OnReadyToThrow?.Invoke();
            currentInput = new List<DDR_Direction>();
            OnInputChange?.Invoke();
        }
    }

    void PrintInput()
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < currentInput.Count; i++)
        {
            builder.Append(currentInput[i]);
            builder.Append(' ');
        }
        print(builder.ToString());
    }

    bool CodesAreEqual(List<DDR_Direction> code1, List<DDR_Direction> code2)
    {
        if (code1.Count != code2.Count)
        {
            return false;
        }
        for (int i = 0; i < code1.Count; i++)
        {
            if (code1[i] != code2[i])
            {
                return false;
            }
        }
        return true;
    }
}
