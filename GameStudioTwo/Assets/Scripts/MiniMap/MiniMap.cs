using UnityEngine;
using System.Collections;

public class MiniMap: MonoBehaviour {
	public RenderTexture MiniMapTexture;
	private float offset; 

	void Awake(){
		offset = 10;
	}

	void OnGUI(){
		if(Event.current.type == EventType.Repaint){
			Graphics.DrawTexture(new Rect(Screen.width - 256 - offset,offset,256,256),MiniMapTexture);
		}
	}
}
