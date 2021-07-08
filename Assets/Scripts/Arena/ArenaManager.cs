using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class ArenaManager : MonoBehaviour
{
    private NavMeshSurface2d navMeshSurface;
    [SerializeField] private GameObject player, drop, lifePoint;
    [SerializeField] private Drop rockPickup;
    [SerializeField] private GameObject[] enemies = new GameObject[5];
    public float Timer { get; private set; }
    public int Round { get; private set; }

    private bool lost = false;
    [SerializeField] private float nextRoundTimerMax, spawnerRadius;
    [SerializeField] private int baseStartEnemies;
    private float nextRoundTimer;


    //UI
    [SerializeField] private TextMeshProUGUI roundText, timerText, nextRoundText;


    private void Start()
    {
        for(int x = 0; x < Random.Range(5, 10); x++)
        {
            Vector2 randomDirection = (Vector2)transform.position + Random.insideUnitCircle * spawnerRadius;

            NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, spawnerRadius, NavMesh.AllAreas);

            GameObject rock = Instantiate(drop, hit.position, Quaternion.identity);
            rock.GetComponent<Pickup>().dropSO = rockPickup;
        }
        StartNextRound();
        nextRoundTimer = nextRoundTimerMax;
    }
    private void Update()
    {
        if (!lost)
        {
            Timer += Time.deltaTime;

            timerText.text = FormatTimer(Timer);
        }

        if (nextRoundTimer > 0)
        {
            nextRoundTimer -= Time.deltaTime;
            
            nextRoundText.text = "Next round in " + FormatTimer(nextRoundTimer);
        }
        else
        {
            StartNextRound();
        }
    }

    private void StartNextRound()
    {
        Round++;
        nextRoundTimer = nextRoundTimerMax;

        roundText.text = "Round " + Round.ToString();

        for (int x = 0; x < Mathf.Round(baseStartEnemies * (Round/2)); x++)
        {
            Vector2 randomDirection = (Vector2)transform.position + Random.insideUnitCircle * spawnerRadius;

            NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, spawnerRadius, NavMesh.AllAreas);

            Instantiate(enemies[Random.Range(0, enemies.Length)], hit.position, Quaternion.identity);
        }

        if (Round % 2 == 0)
        {
            for (int x = 0; x < Mathf.Round(Round / 4); x++)
            {
                Vector2 randomDirection = (Vector2)transform.position + Random.insideUnitCircle * spawnerRadius;

                NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, spawnerRadius, NavMesh.AllAreas);

                Instantiate(lifePoint, hit.position, Quaternion.identity);
            }
        }


    }

    private string FormatTimer(float timeFloat)
    {
        int timeInSecondsInt = (int)timeFloat;
        int minutes = timeInSecondsInt / 60;
        int seconds = timeInSecondsInt - (minutes * 60);

        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnerRadius);
    }

}
