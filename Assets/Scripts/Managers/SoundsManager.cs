using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.Collections;

public class SoundsManager: BaseManager<SoundsManager>{
    #region Variables
    private const string XML_PATH           = "Data/XML/";
    private const string NAME_FIELD_NAME    = "name";
    private const string VOLUME_FIELD_NAME  = "volume";

    #region SFX
    private const string SFX_PATH           = "Sounds/SFX/";
    private const string SFX_FILE_NAME      = "SFX_setup";
    private const string SFX_FIELD_NAME     = "SFX";
    private const int SFX_SOURCES_NB        = 10;
    private List<AudioSource> m_SFXSources;
    private Dictionary<string, MyAudioClip> m_SFXDictionary;
    #endregion

    #region Musics
    private const string MUSICS_PATH        = "Sounds/Musics/";
    private const string MUSICS_FILE_NAME   = "musics_setup";
    private const string MUSICS_FIELD_NAME  = "MUSICS";
    private const string MENU_MUSIC_NAME    = "loop_menu";
    private const int MUSICS_SOURCES_NB     = 2;
    private const float m_FadeDuration      = 2;
    private List<AudioSource> m_MusicsSources;
    private Dictionary<string, MyAudioClip> m_MusicsDictionary;
    private int m_CurrentMusicID    = 0;
    private int m_LastMusicID       = 1;
    private string m_CurrentMusicName;
    private string m_LastMusicName;
    #endregion
    #endregion

    #region Initialisation
    override protected IEnumerator CoroutineStart() {
        LoadSFX();
        LoadMusics();
        isReady = true;

        yield return true;
    }

    private void LoadSFX() {
        TextAsset l_Resource    = Resources.Load(XML_PATH + SFX_FILE_NAME) as TextAsset;
        if (l_Resource == null) LoadFail(XML_PATH, SFX_FILE_NAME);
        XmlDocument l_XML       = new XmlDocument();
        m_SFXSources            = new List<AudioSource>();
        m_SFXDictionary         = new Dictionary<string, MyAudioClip>();
        l_XML.LoadXml(l_Resource.ToString());

        foreach (XmlNode node in l_XML.GetElementsByTagName(SFX_FIELD_NAME)) {
            if (node.NodeType != XmlNodeType.Comment) {
                AudioClip l_SFX = Resources.Load(SFX_PATH + node.Attributes[NAME_FIELD_NAME].Value) as AudioClip;
                if (l_SFX == null) LoadFail(SFX_PATH, node.Attributes[NAME_FIELD_NAME].Value);
                m_SFXDictionary.Add(node.Attributes[NAME_FIELD_NAME].Value, new MyAudioClip(l_SFX, float.Parse(node.Attributes[VOLUME_FIELD_NAME].Value)));
            }
        }

        for (int cptSource = 0; cptSource < SFX_SOURCES_NB; cptSource++) {
            AddSource(m_SFXSources, false);
        }
    }

    private void LoadMusics() {
        TextAsset l_Resource = Resources.Load(XML_PATH + MUSICS_FILE_NAME) as TextAsset;
        if (l_Resource == null) LoadFail(XML_PATH, MUSICS_FILE_NAME);
        XmlDocument l_XML   = new XmlDocument();
        m_MusicsSources     = new List<AudioSource>();
        m_MusicsDictionary  = new Dictionary<string, MyAudioClip>();
        l_XML.LoadXml(l_Resource.ToString());

        foreach (XmlNode node in l_XML.GetElementsByTagName(MUSICS_FIELD_NAME)) {
            if (node.NodeType != XmlNodeType.Comment) {
                AudioClip l_Musics = Resources.Load(MUSICS_PATH + node.Attributes[NAME_FIELD_NAME].Value) as AudioClip;
                if (l_Musics == null) LoadFail(MUSICS_PATH, node.Attributes[NAME_FIELD_NAME].Value);
                m_MusicsDictionary.Add(node.Attributes[NAME_FIELD_NAME].Value, new MyAudioClip(l_Musics, float.Parse(node.Attributes[VOLUME_FIELD_NAME].Value)));
            }
        }

        for (int cptSource = 0; cptSource < MUSICS_SOURCES_NB; cptSource++) {
            AddSource(m_MusicsSources, true);
            m_MusicsSources[cptSource].volume = 0;
        }
    }

    private void AddSource(List<AudioSource> p_Sources, bool p_IsLooping) {
        AudioSource l_AudioSource   = gameObject.AddComponent<AudioSource>();
        p_Sources.Add(l_AudioSource);
        l_AudioSource.loop          = p_IsLooping;
        l_AudioSource.playOnAwake   = false;
    }

    private void LoadFail(string p_PathName, string p_FileName) {
        Debug.LogError("SfxManager throw an error : " + p_PathName + p_FileName + " not found");
    }
    #endregion

    #region SFX Managment
    public void PlaySfx(string p_SfxName) {
		if (!m_SFXDictionary.ContainsKey(p_SfxName)) return;

        MyAudioClip l_AudioClip     = m_SFXDictionary[p_SfxName];
        AudioSource l_AudioSource   = m_SFXSources.Find(item => !item.isPlaying);

		if (l_AudioSource != null) l_AudioSource.PlayOneShot(l_AudioClip.clip, l_AudioClip.volume);
	}
    #endregion

    #region Musics Managment
    public void PlayMusic(string p_MusicName) {
        m_LastMusicID       = m_CurrentMusicID;
        m_LastMusicName     = m_CurrentMusicName;
        m_CurrentMusicID    = (m_CurrentMusicID + 1) % MUSICS_SOURCES_NB;
        m_CurrentMusicName  = p_MusicName;
        
        m_MusicsSources[m_CurrentMusicID].clip = m_MusicsDictionary[m_CurrentMusicName].clip;
        StartCoroutine(FadeCoroutine());
        m_MusicsSources[m_CurrentMusicID].Play();
    }

    IEnumerator FadeCoroutine() {
        float l_ElapsedTime         = 0;
        float l_LastMusicVolume     = m_MusicsSources[m_LastMusicID].volume;

        while (l_ElapsedTime < m_FadeDuration) {
            float l_FadeTimeRatio = l_ElapsedTime / m_FadeDuration;

            m_MusicsSources[m_CurrentMusicID].volume    = Mathf.Lerp(0, m_MusicsDictionary[m_CurrentMusicName].volume, l_FadeTimeRatio);
            m_MusicsSources[m_LastMusicID].volume       = Mathf.Lerp(l_LastMusicVolume, 0, l_FadeTimeRatio);
            l_ElapsedTime                               += Time.deltaTime;

            yield return null;
        }

        m_MusicsSources[m_LastMusicID].Stop();
    }

    /*IEnumerator FadeOutAndStopAll(float p_Delay) {
        yield return new WaitForSeconds(p_Delay + 0.1f);
        float l_ElapsedTime = 0;

        while (l_ElapsedTime < m_FadeDuration) {
            float l_Ratio                           = l_ElapsedTime / m_FadeDuration;
            m_MusicsSources[m_FadeInID].volume      = Mathf.Lerp(0, m_MusicsDictionary[m_CurrentAudioClipName].volume, 1 - l_Ratio);
            m_MusicsSources[1 - m_FadeInID].volume  = Mathf.Lerp(0, m_MaxVolumes[1 - m_FadeInID], 1 - l_Ratio);
            l_ElapsedTime += Time.deltaTime;

            yield return null;
        }

        m_MusicsSources[m_FadeInID].volume = 0;
        m_MusicsSources[m_FadeInID].Stop();
        m_MusicsSources[1 - m_FadeInID].volume = 0;
        m_MusicsSources[1 - m_FadeInID].Stop();
    }
    
    public void StopAll (float delay) {
		StartCoroutine(FadeOutAndStopAll(delay));
	}

	public void StopAllRightAway () {
		StopAllCoroutines();
		m_AudioSources[m_FadeInID].volume = 0;
		m_AudioSources[1-m_FadeInID].volume = 0;
		m_AudioSources[1-m_FadeInID].Stop();
		m_AudioSources[m_FadeInID].Stop();
	}*/
    #endregion

    protected override void MainScreen() {
        PlayMusic(MENU_MUSIC_NAME);
    }
}