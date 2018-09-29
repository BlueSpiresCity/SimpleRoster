using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class MainController : MonoBehaviour {
	public Entry entryPrefab;
	public RectTransform scrollContent;
	
	public InputField teamName;
	
	private List<Entry> entries;
	
	void Start(){
		LoadRoster();
	}
	
	public void AddEntry(){
		Entry entry = Instantiate(entryPrefab) as Entry;
		entry.transform.SetParent(scrollContent);
		entry.transform.localScale = Vector3.one;
		
		entries.Add(entry);
	}
	
	void AddEntry(Dictionary<string, string> entryInfo){
		Entry entry = Instantiate(entryPrefab) as Entry;
		entry.transform.SetParent(scrollContent);
		entry.transform.localScale = Vector3.one;
		
		entry.modelName.text = entryInfo["modelName"];
		entry.points.text = entryInfo["points"];
		entry.weapons.text = entryInfo["weapons"];
		entry.specialism.text = entryInfo["specialism"];
		
		entries.Add(entry);
	}
	
	public void SaveRoster(){
		List<string> payload = new List<string>();
		foreach(Entry entry in entries){
			payload.Add(entry.CreateJSON());
		}
		
		string json = JsonConvert.SerializeObject(payload, Formatting.Indented);
		
		PlayerPrefs.SetString("teamName", teamName.text);
		PlayerPrefs.SetString("roster", json);
	}
	
	public void DeleteRoster(){
		foreach(Entry entry in entries){
			Destroy(entry.gameObject);
		}
		
		entries = new List<Entry>();
		
		teamName.text = "";
		
		PlayerPrefs.DeleteKey("teamName");
		PlayerPrefs.DeleteKey("roster");
	}
	
	void LoadRoster(){
		 entries = new List<Entry>();
		
		if(PlayerPrefs.HasKey("teamName")){
			teamName.text = PlayerPrefs.GetString("teamName");
		}
		
		if(PlayerPrefs.HasKey("roster")){
			string json = PlayerPrefs.GetString("roster");
			
			List<string> load = JsonConvert.DeserializeObject<List<string>>(json);
			
			foreach(string entryJSON in load){
				Dictionary<string, string> entryInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(entryJSON);
				AddEntry(entryInfo);
			}
		}
	}
}
