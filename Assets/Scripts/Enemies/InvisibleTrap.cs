using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleTrap : MonoBehaviour
{
    public float hideShowDuration = 1f;
    private bool isChangingAlpha = false;

    [SerializeField] private SpriteRenderer[] renderers;

    private ChaserEnemy ce;
    private AttackWithinRange awr;
    void Start()
    {
        renderers = GetComponentsInChildren<SpriteRenderer>();



            for (int x = 0; x < renderers.Length; x++)
            {
            Debug.Log(renderers[x]);
                Color temp = renderers[x].color;
                temp.a = 0;
                renderers[x].color = temp;
            }

        awr = GetComponent<AttackWithinRange>();
        awr.canShoot = false;
        ce = GetComponent<ChaserEnemy>();
        ce.OnSightEvent.AddListener(delegate { StartCoroutine(ShowHideRenderer(0, 1)); });
        ce.LostSightEvent.AddListener(delegate { StartCoroutine(ShowHideRenderer(1, 0)); });
        ce.stayHide = true;
    }

    IEnumerator ShowHideRenderer(int start, int target)
    {

            float journey = 0;
            while (journey <= hideShowDuration)
            {

                journey += Time.deltaTime;


                float percent = Mathf.Lerp(start, target, journey / hideShowDuration);

                //float percent = Mathf.Clamp(journey / hideShowDuration, start, target);


                for (int x = 0; x < renderers.Length; x++)
                {
                    Color temp = renderers[x].color;
                    temp.a = percent;
                    renderers[x].color = temp;
                }

                yield return null;
            }

            if (target == 1)
            {

                ce.stayHide = false;
                awr.canShoot = true;

            }
            else
            {
                ce.stayHide = true;
                awr.canShoot = false;

            }
        
    }
}
