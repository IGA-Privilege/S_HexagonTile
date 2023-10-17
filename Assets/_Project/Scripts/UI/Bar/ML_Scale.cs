using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Jahaha.MethodList
{
    public static class ML_Scale
    {
        public static void Pop(float startOffset, float firstValue, float secondValue, float thirdValue, Transform targetTrans, float time)
        {
            float timeUnit = time / 10;
            targetTrans.localScale = new Vector3(startOffset, startOffset, startOffset);
            Sequence s = DOTween.Sequence();
            s.Append(targetTrans.DOScale(firstValue, timeUnit * 5));
            s.Append(targetTrans.DOScale(secondValue, timeUnit * 3));
            s.Append(targetTrans.DOScale(thirdValue, timeUnit * 2));
        }
    }

}