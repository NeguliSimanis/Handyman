using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region MOVEMENT
    private bool isWalking = false;
    private Vector2 targetPosition;
    private Vector2 dirNormalized;
    #endregion

    private void Start()
    {
        if (PlayerData.current == null)
        {
            PlayerData.current = new PlayerData();
        }   
    }

    void Update ()
    {
        if (Input.GetMouseButton(0))
        {
            GetTargetPositionAndDirection();
            CheckIfPlayerIsWalking();
        }
        if (isWalking)
        {
            CheckIfPlayerIsWalking();
            MovePlayer();
        }
    }

    void CheckIfPlayerIsWalking()
    {
        if (Vector2.Distance(targetPosition, transform.position) <= 0.02f)
        {
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }
    }


    void GetTargetPositionAndDirection()
    {
        targetPosition = Input.mousePosition;
        targetPosition = Camera.main.ScreenToWorldPoint(targetPosition);
        GetDirNormalized(targetPosition);
    }

    void GetDirNormalized(Vector2 sourceVector)
    {
        dirNormalized = new Vector2(sourceVector.x - transform.position.x, sourceVector.y - transform.position.y);
        dirNormalized = dirNormalized.normalized;
    }

    void MovePlayer()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y) + dirNormalized * PlayerData.current.moveSpeed * Time.deltaTime;
    }
}
