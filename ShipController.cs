using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour{
	[SerializeField]
	private float thrust = 5f;	//propulsão
	
	[SerializeField]
	private float torque = 5f;	//torque
	
	[SerializeField]
	private GameObject bulletPrefab;	//prefab do projétil
	
	[SerializeField]
	private Transform fireSpotTrans;	//transform de referência, de posição e orientação, para criação de uma nova bullet
	
	[SerializeField]
	private int maxLife = 3;	//definindo a vida máxima, que será setada no início do jogo também
	
	int life;	//Qtd. Vida
	Rigidbody2D rb2d;	//Componente Rigidbody2D
	Camera cam;	//Camera principal;
	Vector2	rightTopLim;	//limite superior direito da câmera
	Vector2 leftBottomLim;	//limite inferior esquerdo da câmera
	
	//Executa 1 vez no início
	void Start(){
		life = maxLife;
		rb2d = GetComponent<Rigidbody2D>();
		cam = Camera.main;
		InitCamValues();
	}
	
	//Executa 1 vez em todos os quadros. Sincronizado com a atualização de frames
	void Update(){
		CheckCamLimits();
		
		if(Input.GetKeyDown(KeyCode.Space)){
			//intanciar novo projétil
			GameObject bulletGo = Instantiate(bulletPrefab, fireSpotTrans.position, fireSpotTrans.rotation);
			bulletGo.tag = gameObject.tag;
		}
	}
	
	//Chamada síncrona com motor de física
	//Intervalo de tempo fixo entre chamadas
	void FixedUpdate(){
		float horizontal = Input.GetAxisRaw("Horizontal");	//-1 esquerda, 0 nada, 1 direita
		float vertical = Input.GetAxisRaw("Vertical");	//-1 baixo, 0 nada, 1 cima
		
		//Add força baseada no input do jogador. Direção "cima" local
		rb2d.AddRelativeForce(vertical * thrust * Vector2.up);
		//Add rotação baseada no input do jogador
		//Ajuste de sinal p/ que a relação entre seta clicada e rotação gerada estejam sincronizadas
		rb2d.AddTorque(-horizontal * torque);		
	}
	
	void OnTriggerEnter2D(Collider2D collision){
		life--;
		HUDManager.UpdateLifeBar(life, maxLife);	//passa os parâmetros para atualizar a lifebar do HUD
		HUDManager.UpdateScoreText(score);
		if(life == 0)
			Destroy(gameObject);
	}
	
	//Inicializar limites de acordo com a visão da câmera no mapa
	void InitCamValues(){
		rightTopLim = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		leftBottomLim = cam.ScreenToWorldPoint(new Vector3(0, 0, 0));
	}
	
	//Verifica posição da nava em relação aos limites da câmera e teletransporta para lado oposto (estilo pacman)
	void CheckCamLimits(){
		Vector3 pos = transform.position;
		
		if(transform.position.x > rightTopLim.x)
        {
            pos.x = leftBottomLim.x;
        }
        else if(transform.position.x < leftBottomLim.x)
        {
            pos.x = rightTopLim.x;
        }
        else if(transform.position.y > rightTopLim.y)
        {
            pos.y = leftBottomLim.y;
        }
        else if(transform.position.y < leftBottomLim.y)
        {
            pos.y = rightTopLim.y;
        }

        transform.position = pos;
	}	
}