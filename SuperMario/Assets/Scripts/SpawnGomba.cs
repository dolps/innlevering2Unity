using UnityEngine;
using System.Collections;

public class SpawnGomba : MonoBehaviour {
	public GameObject gomba;
	public Transform spawnPoint2;
	private static bool spawned;
	
	void OnTriggerEnter2D(){
		if (!spawned)
			CreateGomba ();
		spawned = true;
	}
	private void CreateGomba(){
		Instantiate (gomba,gomba.transform.position,Quaternion.identity);
		Instantiate (gomba, spawnPoint2.position, Quaternion.identity);
	}
}
