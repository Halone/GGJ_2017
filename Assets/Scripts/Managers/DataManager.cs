using System.Collections;
using System.Collections.Generic;

public class DataManager: BaseManager<DataManager> {
    #region Variables
    private const string JSON_PATH = "Data/Jsons/";
    #endregion

    #region Initialisation & Destroy
    protected override IEnumerator CoroutineStart() {
        isReady = true;

        yield return true;
    }

    /*private void LoadLevels() {
        m_Levels            = new Dictionary<int, JSONObject>();
        m_LevelsMissions    = new Dictionary<int, string>();

        for (int cptLevel = 1; cptLevel <= LEVEL_NB; cptLevel++) {
            TextAsset l_JsonLevel = Resources.Load(JSON_PATH + LEVEL_FILE_NAME + cptLevel) as TextAsset;

            if (l_JsonLevel == null) {
                DebugError(LEVEL_FILE_NAME + cptLevel + " not found");
                break;
            }

            JSONObject l_LevelData = new JSONObject(l_JsonLevel.ToString());
            m_LevelsMissions.Add(cptLevel, l_LevelData.GetField(INFO_FIELD_NAME).GetField(LEVEL_MISSION_FIELD_NAME).str);
            m_Levels.Add(cptLevel, l_LevelData.GetField(DATA_FIELD_NAME));
        }
    }*/

    private List<string> GetStringList(JSONObject l_Object) {
        List<string> l_WeaponList = new List<string>();

        for (int cptWeapon = 0; cptWeapon < l_Object.Count; cptWeapon++) {
            l_WeaponList.Add(l_Object[cptWeapon].str);
        }

        return l_WeaponList;
    }
    #endregion
    
    #region Data Managment
    /*public JSONObject GetLevel(int p_LevelID) {
        if (!m_Levels.ContainsKey(p_LevelID)) DebugError(p_LevelID.ToString() + " can not be found in the dictionnary");
        return m_Levels[p_LevelID];
    }*/
    #endregion
}
 //TODO: refacto partiel en intégrant un fichier de config (nombre de level / noms des levels / ...)
 //TODO: Destroy