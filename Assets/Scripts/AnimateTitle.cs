using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimateTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(Vector2.one, 1f).SetEase(Ease.OutQuad);
        transform.DOMoveY(transform.position.y + 10f,1f)
            .SetLoops(-1,LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
}
