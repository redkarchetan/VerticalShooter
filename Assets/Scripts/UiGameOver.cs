using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// Game over screen.
// Displays last game score.
public class UiGameOver : MonoBehaviour 
{
	public Text _TxtScore;

	void OnEnable()
	{
		if (_TxtScore != null) 
		{
			_TxtScore.text = GameManager.pInstance.pScore.ToString();
		}
	}
}
