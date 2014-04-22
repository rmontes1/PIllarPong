using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class ConstructBoard : MonoBehaviour {

    public int paddleSize = 2, boardCubeSize = 1, boardCubeWidthAmount = 30, paddleAmountWidth = 5, paddleAmountHeight = 3;
    public GameObject leftSideParent, rightSideParent, boardParent;
    public Material leftSideMat, rightSideMat, boardMat, selected;
    public float raiseRate = 0.5f, raiseHeight = 2;

    private GameObject selectedCube;
    private Material lastMaterial;
	// Use this for initialization
	void Start () {
        selectedCube = this.gameObject;
        CreatePaddleGroup(0, 0, true);
        CreateCubeBoard(0, 1 * paddleAmountHeight);
        CreatePaddleGroup(0, ((1 * paddleAmountHeight) + (boardCubeSize * boardCubeWidthAmount)), false);

	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        if (Physics.Raycast(ray, out hit)) {
            if (selectedCube != hit.collider.gameObject) {
                selectedCube.renderer.material = lastMaterial;
                lastMaterial = hit.collider.gameObject.renderer.material;
                selectedCube = hit.collider.gameObject;
                selectedCube.renderer.material = selected;
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            HOTween.To(selectedCube.transform, raiseRate, new TweenParms().Prop("localScale", new Vector3(selectedCube.transform.localScale.x, raiseHeight, selectedCube.transform.localScale.z)).Loops(2, LoopType.Yoyo));//.Ease(EaseType.EaseOutQuad));
            HOTween.To(selectedCube.transform, raiseRate, new TweenParms().Prop("position", new Vector3(selectedCube.transform.position.x, raiseHeight / 2, selectedCube.transform.position.z)).Loops(2, LoopType.Yoyo));//.Ease(EaseType.EaseOutQuad));
        }
	}

    public void CreatePaddleGroup(int startX, int startZ, bool leftSide) {
        for (int i = 0; i < paddleAmountHeight; ++i) {
            for (int j = 0; j < paddleAmountWidth; ++j) {
                GameObject paddle = GameObject.CreatePrimitive(PrimitiveType.Cube);
                paddle.transform.localScale = new Vector3(paddleSize, 1, 1);
                paddle.transform.position = new Vector3(startX + (paddleSize * j), 0, startZ + (1 * i));
                paddle.transform.name = "Paddle (" + (startX + i) + ", " + (startZ + j) + ")";
                paddle.transform.parent = (leftSide) ? leftSideParent.transform : rightSideParent.transform;
                paddle.renderer.material = (leftSide) ? leftSideMat : rightSideMat;
                paddle.AddComponent<BoxCollider>();
            }
        }
    }

    public void CreateCubeBoard(int startX, int startZ) {
        for (int i = 0; i < boardCubeWidthAmount; ++i) {
            for (int j = 0; j < paddleAmountWidth*2; ++j) {
                GameObject board = GameObject.CreatePrimitive(PrimitiveType.Cube);
                board.transform.localScale = new Vector3(boardCubeSize, 1, 1);
                board.transform.position = new Vector3(startX + (boardCubeSize * j) - (0.5f), 0, startZ + (boardCubeSize * i));
                board.transform.name = "Board (" + (startX + i) + ", " + (startZ + j) + ")";
                board.transform.parent = boardParent.transform;
                board.renderer.material = boardMat;

                //make triggers on top of the boards so that when they are hit we can make them bounce up
                BoxCollider collider = board.AddComponent<BoxCollider>();
                collider.size = new Vector3(collider.size.x, raiseHeight, collider.size.z);
                collider.isTrigger = true;
                collider.center = new Vector3(collider.center.x, raiseHeight/2, collider.center.z);
            }
        }
    }
}
