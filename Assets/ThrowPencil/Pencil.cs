﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour {

	// 初期位置
	public Vector3 InitPencilPos { get; private set; }

	// 仮の出目
	public int TmpOutcome { get; private set; }
	// 出目
	public int Outcome { get; private set; }

	// 召喚するモンスターのプレハブ
	[Header("召喚するモンスターのプレハブ")]
	[SerializeField]
	GameObject monsterPrefab;

	public void Init() {
		Outcome = 0;
		TmpOutcome = 0;
		//var rigidbody = GetComponent<Rigidbody>();
		//rigidbody.velocity = Vector3.zero;
		//rigidbody.rotation = Quaternion.identity * Quaternion.FromToRotation(Vector3.forward, Vector3.left);
	}

	private void Awake() {
		Init();
		InitPencilPos = transform.position;
	}

	private void Start() {
	}

	public void StartOutcomeDetection() {
		Debug.Log("StartOutcomeDetection : " + transform.parent.name);

		GetComponentInParent<OperatorController>().StartThrow();

		Init();
		StartCoroutine(OutcomeDetectionCoroutine());
	}

	// 鉛筆投擲フェイズのコルーチン
	IEnumerator OutcomeDetectionCoroutine() {

		var rigidbody = GetComponent<Rigidbody>();

		// 運動時間
		float moveTime = 0;
		// 静止時間
		float restTime = 0;

		while (true) {

			// 運動していたら
			if (rigidbody.velocity.sqrMagnitude > 1) {
				moveTime += Time.deltaTime;
			}

			if (moveTime > 0.01) {
				break;
			}

			yield return null;
		}

		while (true) {
			// 出目表示用の変数に格納
			TmpOutcome = LuckDetermination();
			//Debug.Log(gameObject.name + "出目(仮)" + TmpOutcome);

			// 静止していたら
			if (rigidbody.velocity.sqrMagnitude == 0) {
				restTime += Time.deltaTime;
			}
			// 静止時間が一定値を超えたら
			if (restTime > 0.1) {
				break;
			}

			yield return null;
		}

		// 出目判定を行う
		Outcome = LuckDetermination();
		Debug.Log(gameObject.name + "出目(確定)" + Outcome);

		GetComponentInParent<OperatorController>().StopThrow();

		yield return null;
	}

	Ray ray;

	//出目判定
	public int LuckDetermination() {
		int num = 0;

		ray = new Ray(transform.position, Vector3.up);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 10)) {
			if (hit.collider.tag == "numbers") {
				num = hit.collider.gameObject.GetComponent<number>().num;

				//Debug.Log(gameObject.name + "出目" + num);
			}
		}

		return num;
	}

	// Rayの描画
	private void OnDrawGizmos() {
		Debug.DrawRay(ray.origin, ray.direction, Color.gray);
		Gizmos.DrawRay(ray);
	}

	// モンスターの召喚
	public void SummonMonster() {
		// 召喚されていないなら
	    var monsObj = Instantiate(monsterPrefab, transform.position, Quaternion.identity, transform.parent.transform);
        monsObj.tag = transform.parent.gameObject.tag;

		if (monsObj.gameObject.tag == "Player") {
            monsObj.GetComponent<BaseMonsterBehaviour>().GetSummonMotion(OperatorManager.Instance.PlayerController.MonsterStandPos.position);
        }
        else if(monsObj.gameObject.tag == "CPU") {
            monsObj.GetComponent<BaseMonsterBehaviour>().GetSummonMotion(OperatorManager.Instance.ComputerController.MonsterStandPos.position);
        }
        
	}
}
