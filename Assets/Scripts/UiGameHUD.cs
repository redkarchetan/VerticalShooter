using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Display score and lives.
public class UiGameHUD : MonoBehaviour 
{
	public Text _TxtScore;
	public Text _Lives;

	void Update()
	{
		_TxtScore.text = GameManager.pInstance.pScore.ToString();
		_Lives.text = GameManager.pInstance.pLives.ToString();
	}
}
