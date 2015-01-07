using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using GDataDB;
using GDataDB.Linq;

///
/// !!! Machine generated code !!!
///
[CustomEditor(typeof(Map))]
public class MapEditor : BaseGoogleEditor<Map>
{	
    public override void OnEnable()
    {
        base.OnEnable();
        
        Map data = target as Map;
        
        databaseFields = ExposeProperties.GetProperties(data);
        
        foreach(MapData e in data.dataArray)
        {
            dataFields = ExposeProperties.GetProperties(e);
            pInfoList.Add(dataFields);
        }
    }
    
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        //DrawDefaultInspector();
        if (GUI.changed)
        {
            pInfoList.Clear();
            
            Map data = target as Map;
            foreach(MapData e in data.dataArray)
            {
                dataFields = ExposeProperties.GetProperties(e);
                pInfoList.Add(dataFields);
            }
            
            EditorUtility.SetDirty(target);
            Repaint();
        }
    }
    
    public override bool Load()
    {
        if (!base.Load())
            return false;
        
        Map targetData = target as Map;
        
        var client = new DatabaseClient(username, password);
        var db = client.GetDatabase(targetData.SheetName) ?? client.CreateDatabase(targetData.SheetName);	
        var table = db.GetTable<MapData>(targetData.WorksheetName) ?? db.CreateTable<MapData>(targetData.WorksheetName);
        
        List<MapData> myDataList = new List<MapData>();
        
        var all = table.FindAll();
        foreach(var elem in all)
        {
            MapData data = new MapData();
            
            data = Cloner.DeepCopy<MapData>(elem.Element);
            myDataList.Add(data);
        }
                
        targetData.dataArray = myDataList.ToArray();
        
        EditorUtility.SetDirty(targetData);
        AssetDatabase.SaveAssets();
        
        return true;
    }
}
