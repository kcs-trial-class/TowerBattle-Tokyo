using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGenerator : AnimalGeneratorBase
{
    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        if (IsGameOver)
        {
            return;
        }

        if (IsMove(Animal.MovingLists))
        {
            return;
        }

        if (!isGenerate_)
        {
            StartCoroutine(GenerateAnimal());
            isGenerate_ = true;
            return;
        }

        Vector2 vec = new Vector2(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
            pivotHeight_);

        if (Input.GetMouseButtonUp(0))
        {
            generateAnimal_.transform.position = vec;
            generateAnimal_.Rigidbody.isKinematic = false;
            isFall_ = true;
        }
        else if (Input.GetMouseButton(0))
        {
            // 押している最中はオブジェクトとマウスの位置を合わせる.
            generateAnimal_.transform.position = vec;
        }
    }
}