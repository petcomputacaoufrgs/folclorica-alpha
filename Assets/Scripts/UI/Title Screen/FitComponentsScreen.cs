using UnityEngine;
using System.Collections;
using System.Data;


public class FitComponentsScreen : MonoBehaviour {

	public Camera camera;
	public Texture btnTexture;
	public int btStartOffsetY = 100;
	public int btStartHeight = 40;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnGUI () {
	
		SpriteRenderer bckgSprite = this.GetComponent<SpriteRenderer>();

		if(bckgSprite == null)
			return;

		if (!btnTexture) {
			Debug.LogError("Nao ha imagem associada ao botao");
			return;
		}

		float bckgWidth = (float) bckgSprite.sprite.bounds.size.x;
		float bckgHeight = (float) bckgSprite.sprite.bounds.size.y;


		float worldScreenH = (float) camera.orthographicSize * 2.0f;
		float worldScreenW = (float) worldScreenH / Screen.height * Screen.width;

		float w = worldScreenW / bckgWidth;
		float h = worldScreenH / bckgHeight;

		float scale = Mathf.Min(w, h);



		this.transform.localScale = new Vector3(scale, scale, 1);

		int recHeight = btStartHeight;
		int recWidth = btnTexture.width / (btnTexture.height / recHeight);
		Vector3 screenPos = new Vector3(Screen.width/ 2 - recWidth/2, Screen.height/2 + btStartOffsetY, 1);


		Rect btRect = new Rect(screenPos.x, screenPos.y, recWidth, recHeight);

		//Debug.Log("W: " + screenPos.x + " H: " + screenPos.y);

		if (GUI.Button(btRect, btnTexture)){

			CheckPlayerData localPlayer = new CheckPlayerData();

			bool foundPlayer = localPlayer.CheckForLocalPlayer();

			if(!foundPlayer){

				Debug.Log("Nenhum registro encontrado...");
				Debug.Log("Registrando novo usuario...");

			}

			Application.LoadLevel("Scene - Level 01 - South");

			//LocalDBAccess db = new LocalDBAccess();

			//db.OpenConnection();

			/*SqliteDatabase sqlDB = new SqliteDatabase("local-DB-folclorica-01.sqlite3"); 
			DataTable table = sqlDB.ExecuteQuery("SELECT * FROM USUARIO");

			for(int i = 0; i < table.Rows.Count; i++){
				Debug.Log(table.Rows[i].ItemArray[0] +  " " + table.Rows[i].ItemArray[1]);
			}*/

			//Application.LoadLevel("Level 01 - South");
			//Debug.Log("Clicked the button with an image");


		}
		/*if(GUI.Button(btStartImg, "MUAHAHHAHAHHA")){

			Debug.Log("Click");
		}*/




	}
}
