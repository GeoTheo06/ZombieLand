public class playerManager : MonoBehaviour
{
	GameObject player, gameManager, camera1;

	gameManager1 gameManagerScript;
	playerMovement playerMovementScript;
	cameraMovement cameraMovementScript;
	public int playerHealth;
	public bool playerDead = false;
	GameObject groundCheck, ceilingCheck;
	CharacterController characterController;

	public Animator cameraDie;
	Vector3 startOfCharacterController, endOfCharacterController;

	private void Start()
	{
		player = GameObject.Find("player1");
@ -24,38 + 22,22 @@ public class playerManager : MonoBehaviour
		playerMovementScript = player.GetComponent<playerMovement>();
		gameManagerScript = gameManager.GetComponent<gameManager1>();
		cameraMovementScript = camera1.GetComponent<cameraMovement>();
		groundCheck = GameObject.Find("groundCheck");
		ceilingCheck = GameObject.Find("ceilingCheck");
		characterController = GameObject.Find("player1").GetComponent<CharacterController>();
	}

private void Update()
{
	if (groundCheck.transform.position != startOfCharacterController || ceilingCheck.transform.position != endOfCharacterController)
	{
		set_groundCheck_ceilingCheck_position();
	}
	playerDies();
}

void playerDies()
{
	if (playerHealth <= 0)
	{
		playerDies();
		playerMovementScript.playerDying = true; // starting animation for dying
		cameraMovementScript.enabled = false; //disabling camera control
		gameManagerScript.gameOver = true; //telling the game manager that the game has finished
		cameraDie.SetBool("isDying", true);
		playerDead = true;
	}
}
void set_groundCheck_ceilingCheck_position()
{
	//set groundCheck, ceilingCheck position
	startOfCharacterController = characterController.transform.position;
	startOfCharacterController = new Vector3(characterController.transform.position.x, 0, characterController.transform.position.z);
	endOfCharacterController = new Vector3(characterController.transform.position.x, characterController.center.y + characterController.height, characterController.transform.position.z);
	groundCheck.transform.position = startOfCharacterController;
	ceilingCheck.transform.position = endOfCharacterController;
}
void playerDies()
{
	playerMovementScript.playerDying = true; // starting animation for dying
	cameraMovementScript.enabled = false; //disabling camera control
	gameManagerScript.gameOver = true; //telling the game manager that the game has finished
	cameraDie.SetBool("isDying", true);
	playerDead = true;
}