using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class Entry : MonoBehaviour {
	public InputField modelName;
	public InputField points;
	public InputField weapons;
	public InputField specialism;
	
	public ToggleGroup experience;
	public ToggleGroup fleshWounds;
	
	public Toggle convalescence;
	public Toggle newRecruit;
	
	public string CreateJSON(){
		Dictionary<string, string> payload = new Dictionary<string, string>();
		payload.Add("modelName", modelName.text);
		payload.Add("points", points.text);
		payload.Add("weapons", weapons.text);
		payload.Add("specialism", specialism.text);
		
		//TODO - Toggle stuff
		
		string json = JsonConvert.SerializeObject(payload);
		
		return json;
	}
	
	public void TallyPoints(){
		//
	}
}
