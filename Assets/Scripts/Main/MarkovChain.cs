using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class MarkovChain : MonoBehaviour
{
    public static MarkovChain instance;

    public List<List<string>> transitionName = new List<List<string>>();
    public List<List<double>> transitionMatrix = new List<List<double>>();
    public List<List<double>> tempTransitionMatrix = new List<List<double>>();

    public List<string> states;
    public List<string> tempStates = new List<string>();

    public Dictionary<string, List<double>> weather = new Dictionary<string, List<double>>();

    public int[,] totalTransitionName;
    public string[] statesPerTotalDays;
    public int iterate, days;
    public bool flagBreak;

    WeightRandom<string> rng = new WeightRandom<string>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateTransitionMatrixAndName();
        WeatherForecast(days);

        DontDestroyOnLoad(gameObject);
    }

    void GenerateTransitionMatrixAndName()
    {
        totalTransitionName = new int[states.Count, states.Count];
        for (int i = 0; i < states.Count; i++)
        {
            transitionName.Add(new List<string>());
            for (int j = 0; j < states.Count; j++)
            {
                transitionName[i].Add(states[i] + states[j]);
            }
        }

        for (int i = 0; i < iterate; i++)
        {
            int randomIndex = Random.Range(0, states.Count);
            tempStates.Add(states[randomIndex]);
        }

        for (int i = 0; i < tempStates.Count; i++)
        {
            if (i != 0)
            {
                for (int x = 0; x < states.Count; x++)
                {
                    for (int y = 0; y < states.Count; y++)
                    {
                        if (transitionName[x][y] == tempStates[i - 1] + tempStates[i])
                        {
                            totalTransitionName[x, y]++;
                        }
                    }
                }
                tempStates[i - 1] = tempStates[i - 1] + tempStates[i];
            }
        }

        for (int i = 0; i < states.Count; i++)
        {
            int result = 0;
            transitionMatrix.Add(new List<double>());
            for (int j = 0; j < states.Count; j++)
            {
                result += totalTransitionName[i, j];
            }

            for (int k = 0; k < states.Count; k++)
            {
                transitionMatrix[i].Add(System.Math.Round(totalTransitionName[i, k] / (double)result, 3));
                // Debug.Log(transitionMatrix[i][k] + "% " + transitionName[i][k]);
            }
        }
        tempTransitionMatrix = transitionMatrix;
    }

    public void WeatherForecast(int days)
    {
        int randomIndex = Random.Range(0, states.Count);
        string weatherToday = states[randomIndex];
        int indexTemp = 0;

        statesPerTotalDays = new string[days];

        for (int i = 0; i < days; i++)
        {
            /*for (int j = 0; j < states.Count; j++)
            {
                for (int k = 0; k < states.Count; k++)
                {
                    Debug.Log(i + " " + transitionMatrix[j][k] + "% " + transitionName[j][k]);
                }
            }*/

            indexTemp = states.FindIndex(a => a.StartsWith(weatherToday));
            weather.Add(weatherToday + "." + i.ToString(), transitionMatrix[indexTemp]);
            statesPerTotalDays[i] = weatherToday;

            for (int x = 0; x < states.Count; x++)
            {
                rng.AddEntry(states[x], transitionMatrix[indexTemp][x]);
            }
            weatherToday = rng.GetRandom();
            rng.RemoveAllList();
            transitionMatrix = MatrixMultiplication(transitionMatrix, tempTransitionMatrix);
        }
    }

    List<List<double>> MatrixMultiplication(List<List<double>> matrixA, List<List<double>> matrixB)
    {
        double[,] tempTotal = new double[states.Count, states.Count];
        List<List<double>> totalTransMat = new List<List<double>>();
        for (int i = 0; i < states.Count; i++)
        {
            totalTransMat.Add(new List<double>());
            for (int j = 0; j < states.Count; j++)
            {
                tempTotal[i, j] = 0;

                for(int k = 0; k < states.Count; k++)
                {
                    tempTotal[i, j] += matrixA[i][k] * matrixB[k][j];
                }

                totalTransMat[i].Add(tempTotal[i, j]);
                // Debug.Log(tempTotal[i, j]);
            }
        }

        return totalTransMat;
    }
}
