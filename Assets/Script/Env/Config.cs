using UnityEngine;
using System;

public static class Config
{

	public const int              		TARGET_FRAMERATE 				= 60;
	public const int              		VSYNC_COUNT 					= 0;
	public const bool             		ASSETBUNDLE_AUTO_SAVE 			= true;

	public const string           		UNITY_VERSION 					= "4.5.5f1";

	public const string           		SCENE_COMMON 					= "CommonScene";
	public const string           		SCENE_MAIN 						= "main_scene";

	public const string 		  		VIEWS_PREFABS_PATH    			= "Views";
	public const string			  		MAIN_VIEW 						= "MainGameView";
	public const string			  		FB_VIEW 						= "Facebook";
	public const string			  		GAME_PLAY_VIEW 					= "GamePlay";
	public const string			  		LOADING_VIEW					= "Loading"; 	
	public const string			  		GLOBAL_VIEW						= "Global";

	public const string           		TAG_COMMON 						= "CommonScene";
	public const string           		TAG_MAIN_CAMERA 				= "MainCamera";
	public const string           		TAG_CHARACTER         			= "CHARACTER";
	public const string           		TAG_UNIT              			= "Unit";
	public const string           		TAG_UNTAGGED          			= "Untagged";
	public const string           		TAG_DECK              			= "Deck";

	public static readonly int    		LAYER_NONE            			= LayerMask.NameToLayer("None");
	public static readonly int    		LAYER_DEFAULT         			= LayerMask.NameToLayer("Default");
	public static readonly int   		LAYER_UI              			= LayerMask.NameToLayer("UI");
	public static readonly int    		LAYER_LABEL           			= LayerMask.NameToLayer("Label");
	public static readonly int    		LAYER_MONSTER         			= LayerMask.NameToLayer("Monster");

	public const int			  		COUNT_OF_MONSTERS     			= 5;	
	public const int					COUNT_OF_TROOPS					= 3;

	public const string 				WINDOW_PATH						= "Prefab/Windows/";
	public const string			  		MONSTER_RESOURCE_PREFIX			= "Prefab/Monster/";	
	public const string					DIALOG_RESOURCE_PREFIX			= "Prefab/Dialog/";
	public const string					PARTICLE_RESOURCE_PREFIX		= "Prefab/particle/";
	public const string					UI_RESOURCE_PREFIX				= "Prefab/UI/";
	public const string 				EFFECT_PATH						= "Prefab/Effect/";
	public const string					BACKGROUND_TEXTURE_PREFIX		= "Texture/Background/bg";


	public static readonly Vector3 		HEADER_IN 						= new Vector3 (0, 360, 0);
	public static readonly Vector3 		HEADER_OUT 						= new Vector3 (0, 550, 0);
	public static readonly Vector3 		FOOTER_IN 						= new Vector3 (0, -360, 0);
	public static readonly Vector3 		FOOTER_OUT 						= new Vector3 (0, -550, 0);

	public static readonly Vector3		CHARACTER_POSITION				= new Vector3(-100, 290, 0);
	public static readonly Vector3 		CHARACTER_DIRECTION				= Vector3.zero;
	public static readonly Vector3 		MONSTER_POSITION				= new Vector3(30, 300, 0);		 
	public static readonly Vector3		COMMON_LOCAL_SCALE				= new Vector3(0.8f,0.8f,1);
	public static readonly Vector3[]	TROOP_POSITION					= {new Vector3(-100, 290, 0), new Vector3(130, 290, 0) , new Vector3(200, 290, 0) };
	public const float			  		TWEEN_DURATION					= 0.5f;

	
	public const string					COMBO_ANIM						= "ComboAnim";
	public const string					FEVER_ANIM						= "FeverAnim";
	public const string					COUNT_DOWN_ANIM					= "CountDown";
	public const string					READY_GO_ANIM					= "ReadyGo";

	public const string			  		ENTRY_ANIM						= "Monster.Entry";
	public const string			  		ATTACKED_ANIM					= "Monster.Attacked";
	public const string			  		DIE_ANIM						= "Monster.Die";
	public const string			  		IDLE_ANIM						= "Monster.Idle";

	public const int			  		PARTICLE_NUM					= 10;

	public const string					CURRENT_MONSTER_INITIAL_HP_KEY	= "CurrentMonsterInitialHP";
	public const string					CURRENT_MONSTER_CURRENT_HP_KEY	= "CurrentMonsterHP";
	public const string					CURRENT_MONSTER_NAME			= "CurrentMonsterName";
	public const string 				FB_ID_DEFAULT					= "123456789";

	//present of game
	public const int 					BELLY_FB_INVITE					= 1000 ;// when invite fb, get 1000belly
	public const int					BELLY_FB_SHARE					= 300; // when share fb,get 300 belly
}