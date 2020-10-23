using System.Collections;
using System.Collections.Generic;
using UnityEnginhe;
using Engine.UI;

public class HUDManager : MonoBehaviour{
	[SerializeField]
	private Image contentLifebar;
	[SerializeField]
	private Text scoreText;
	
	public void UpdateLifeBar(int life, int maxLife){
		contentLifebar.fillAmount = (float)life / maxLife;
	}
	
	public void UpdateScoreText(int score){
		scoreText.text = "Score: " + score;
	}
}