using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class RaisePiece : MonoBehaviour {
    public float raiseRate = 0.5f, raiseHeight = 2f;

    private float lastPosition, lastScale;

    void OnTriggerEnter(Collider other) {
        lastScale = gameObject.transform.localScale.y;
        HOTween.To(gameObject.transform, raiseRate, new TweenParms().Prop("localScale", new Vector3(gameObject.transform.localScale.x, raiseHeight, gameObject.transform.localScale.z)));//.Loops(2, LoopType.Yoyo));//.Ease(EaseType.EaseOutQuad));
        lastPosition = gameObject.transform.position.y;
        HOTween.To(gameObject.transform, raiseRate, new TweenParms().Prop("position", new Vector3(gameObject.transform.position.x, raiseHeight / 2, gameObject.transform.position.z)));//.Loops(2, LoopType.Yoyo));//.Ease(EaseType.EaseOutQuad));
    }

    void OnTriggerExit(Collider other) {
        HOTween.To(gameObject.transform, raiseRate, new TweenParms().Prop("localScale", new Vector3(gameObject.transform.localScale.x, 1, gameObject.transform.localScale.z)));//.Loops(2, LoopType.Yoyo));//.Ease(EaseType.EaseOutQuad));
        HOTween.To(gameObject.transform, raiseRate, new TweenParms().Prop("position", new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z)));//.Loops(2, LoopType.Yoyo));//.Ease(EaseType.EaseOutQuad));
    }
}
