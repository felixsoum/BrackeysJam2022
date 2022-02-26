using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher : BaseNPC
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] sprites;

    Player player;
    float alert;
    Vector3 lastPlayerPosition;

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        base.Start();
    }

    protected override void Update()
    {
        bool isAlerted = false;
        if (Vector3.Distance(transform.position, player.transform.position) < 10f)
        {
            var dir = player.transform.position - transform.position;
            RaycastHit hitInfo;
            Physics.Raycast(transform.position, dir, out hitInfo);
            if (hitInfo.collider.gameObject.CompareTag("Player"))
            {
                var distance = Vector3.Distance(lastPlayerPosition, player.transform.position);
                
                if (Vector3.Distance(lastPlayerPosition, player.transform.position) > 0.01f)
                {
                    isAlerted = true;
                }
            }
        }

        if (isAlerted)
        {
            alert += Time.deltaTime * 500f;
        }
        else
        {
            alert -= Time.deltaTime * 50f;
        }


        alert = Mathf.Clamp(alert, 0, 59);
        //Debug.Log(alert);

        int spriteIndex = (int)(alert / 10);
        spriteRenderer.sprite = sprites[spriteIndex];
        lastPlayerPosition = player.transform.position;
        base.Update();
    }
}
