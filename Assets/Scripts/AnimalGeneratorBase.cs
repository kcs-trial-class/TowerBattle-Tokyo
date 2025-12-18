using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimalGeneratorBase : MonoBehaviour
{
    // ゲームオーバー判定.
    public static bool IsGameOver = false; // ゲームオーバー判定.

    [SerializeField] protected Animal[] animals_ = default; // オブジェクト取得配列.
    [SerializeField] protected float pivotHeight_ = 3f; // 生成位置の基準.
    [SerializeField] protected bool isGenerate_ = false; // 生成されているか.
    [SerializeField] protected bool isFall_ = false; // 生成された動物が落下中か.

    protected Animal generateAnimal_ = default; // 動物生成.


    /// <summary>
    /// 
    /// </summary>
    private void OnInitialize()
    {
        IsGameOver = false;
        StartCoroutine(StateReset());
    }

    protected virtual void Awake()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void Start()
    {
        OnInitialize();
    }

    /// <summary>
    /// リセット処理.
    /// </summary>
    /// <returns></returns>
    protected IEnumerator StateReset()
    {
        while (!IsGameOver)
        {
            yield return new WaitUntil(() => isFall_);
            yield return new WaitForSeconds(0.1f);
            isFall_ = false;
            isGenerate_ = false;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected IEnumerator GenerateAnimal()
    {
        yield return null; // １フレーム待機.
        int animalIndex = Random.Range(0, animals_.Length);
        Vector2 pos = new Vector2(0, pivotHeight_);
        // 回転せずに生成.
        generateAnimal_ = Instantiate(animals_[animalIndex], pos, Quaternion.identity);
        // 物理挙動をさせない状態にする.
        generateAnimal_.Rigidbody.isKinematic = true;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="movings"></param>
    /// <returns></returns>
    protected static bool IsMove(List<Moving> movings)
    {
        if (movings == null)
        {
            return false;
        }

        for (int i = 0; i < movings.Count; i++)
        {
            if (movings[i] == null)
            {
                continue;
            }

            if (movings[i].isMove)
            {
                return true;
            }
        }

        return false;
    }
}