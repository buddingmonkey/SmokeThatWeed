using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActiveWeaponUpdater : MonoBehaviour {

	Text uiText;
	string oldWeapon = null;
	Animator animator;

	void Start() {
		animator = GetComponent<Animator> ();
		uiText = GetComponent<Text> ();
		uiText.text = "";
	}
	// Update is called once per frame
	void Update () {
		if (oldWeapon != GameController.activeProjectile) {
			animator.SetTrigger("Flash");
			uiText.text = GameController.activeWeaponText;
			oldWeapon = GameController.activeProjectile;
			switch (GameController.activeProjectile) {
			case "Flame Thrower":
				SfxManager.Instance.PlaySound("AnnounceFlamethrower");
				break;
			case "Flame":
				SfxManager.Instance.PlaySound("AnnounceFlamethrower");
				break;
			case "Lawn Mower":
				SfxManager.Instance.PlaySound("AnnounceLawnmower");
				break;
			case "Bullet":
				SfxManager.Instance.PlaySound("AnnounceRainmaker");
				break;
			}


		}
	}

}
