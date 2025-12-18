using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Animal : MonoBehaviour
{
    // 移動している動物がいないかチェックするリスト.
    public static List<Moving> MovingLists = new List<Moving>();

    private Rigidbody2D rigidbody_ = default;

    // 移動チェック変数.
    private Moving moving_ = new Moving();

    public Rigidbody2D Rigidbody
    {
        get
        {
            if (rigidbody_ == null)
            {
                rigidbody_ = GetComponent<Rigidbody2D>();
            }

            return rigidbody_;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        MovingLists.Add(moving_);
    }

    /// <summary>
    /// 
    /// </summary>
    private void FixedUpdate()
    {
        // 動いているかどうかを判定する.
        if (Rigidbody.velocity.magnitude > 0.01f)
        {
            moving_.isMove = true;
        }
        else
        {
            moving_.isMove = false;
        }
    }
}