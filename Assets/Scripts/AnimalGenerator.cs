using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGenerator : MonoBehaviour
{
    public Animal[] animals_ = default; // オブジェクト取得配列.

    public float pivotHeight_ = 3f; // 生成位置の基準.

    // ゲームオーバー判定.
    public static bool IsGameOver = false; // ゲームオーバー判定.


    private Animal geneAnimal_ = default; // 動物生成.

    public bool isGene_ = false; // 生成されているか.
    public bool isFall_ = false; // 生成された動物が落下中か.

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        OnInitialize();
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnInitialize()
    {
        IsGameOver = false;
        StartCoroutine(StateReset());
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (IsGameOver)
        {
            return;
        }

        if (IsMove(Animal.MovingLists))
        {
            return;
        }

        if (!isGene_)
        {
            StartCoroutine(GenerateAnimal());
            isGene_ = true;
            return;
        }

        Vector2 vec = new Vector2(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            pivotHeight_);

        if (Input.GetMouseButtonUp(0))
        {
            geneAnimal_.transform.position = vec;
            geneAnimal_.Rigid.isKinematic = false;
            isFall_ = true;
        }
        else if (Input.GetMouseButton(0))
        {
            // 押している最中はオブジェクトとマウスの位置を合わせる.
            geneAnimal_.transform.position = vec;
        }
    }

    /// <summary>
    /// リセット処理.
    /// </summary>
    /// <returns></returns>
    IEnumerator StateReset()
    {
        while (!IsGameOver)
        {
            yield return new WaitUntil(() => isFall_);
            yield return new WaitForSeconds(0.1f);
            isFall_ = false;
            isGene_ = false;
        }
    }


    IEnumerator GenerateAnimal()
    {
        yield return null; // １フレーム待機.
        int animalIndex = Random.Range(0, animals_.Length);
        Vector2 pos = new Vector2(0, pivotHeight_);
        // 回転せずに生成.
        geneAnimal_ = Instantiate(animals_[animalIndex], pos, Quaternion.identity);
        // 物理挙動をさせない状態にする.
        geneAnimal_.Rigid.isKinematic = true;
    }


    private static bool IsMove(List<Moving> movings)
    {
        if (movings == null)
        {
            return false;
        }

        foreach (Moving moving in movings)
        {
            if (moving.isMove)
            {
                return true;
            }
        }

        return false;
    }
}

public class Moving
{
    public bool isMove = false;
}