using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundHandler : MonoBehaviour
{

    [SerializeField] List<EnemyRank> Enemies = new List<EnemyRank>();

    /// <summary>
    /// Base difficult used for each round.
    /// </summary>
    [SerializeField] int baseDifficult;

    /// <summary>
    /// Get an array of enemies for determined round.
    /// <para>Each int will be referred as the position in <see cref="Enemies"/> list</para>
    /// </summary>
    public int[] GetEnemiesForRound(int round)
    {
        if (Enemies.Count <= 0)
            return null;

        // Each round will have a max difficult
        // The system will try to fit as many enemies as possible inside this score
        int maxEnemyScore = round * baseDifficult;

        List<int> selectedEnemiesID = new List<int>();

        for (int s = 0; s < Enemies.Count; s++)
        {
            // If we reach any enemy that has more difficult than the max score, we don't need to search more
            if (Enemies[s].enemyDifficulty > maxEnemyScore)
                break;

            selectedEnemiesID.Add(s);
        }

        int selectedEnemiesScore = 0;


            int currentSelectedSum = 0;

            for (int i = 0; i < selectedEnemiesID.Count; i++)
            {
                currentSelectedSum += Enemies[i].enemyDifficulty;
            }


            // Check if is enough
            if(currentSelectedSum > maxEnemyScore)
            {
                // Do a inverse for so we can remove the highest values
                for (int r = selectedEnemiesID.Count-1; r < 0; r--)
                {
                    currentSelectedSum -= Enemies[r].enemyDifficulty;

                    selectedEnemiesID.RemoveAt(r);

                    if(currentSelectedSum <= maxEnemyScore)
                        break;
                    
                }
            }

            // Check how many we can add

            if(currentSelectedSum < maxEnemyScore)
            {
                for (int i = 0; i < selectedEnemiesID.Count; i++)
                {

                    // Do a inverse for so we can try to add by the highest values to the bottom
                    for (int a = selectedEnemiesID.Count - 1; a < 0; a--)
                    {
                        if (Enemies[a].enemyDifficulty + currentSelectedSum > maxEnemyScore)
                            continue; // If by adding this enemy we'll go over the expected, continue to the next 

                        currentSelectedSum += Enemies[a].enemyDifficulty;

                        selectedEnemiesID.Add(a);
                    }
                }
            }

            selectedEnemiesScore = currentSelectedSum; // Check if we're able to get out of the while loop

        Debug.Log($"Difficulty for round {round} is {currentSelectedSum}");
        return selectedEnemiesID.ToArray();

    }

    public void SpawnEnemyAt(int enemyID, Vector3 enemySpawnPos)
    {
        // TODO: Simple Pool
        Instantiate(Enemies[enemyID].enemyPrefab, enemySpawnPos, Quaternion.identity);
    }

}

[System.Serializable]
public struct EnemyRank
{
    public string enemyName;
    public GameObject enemyPrefab;

    /// <summary>
    /// The more the harder
    /// </summary>
    public int enemyDifficulty;
}
