using FMODUnity;
using System.Collections;

public class FMODManager : Singleton<FMODManager> {
    public StudioEventEmitter BTN_Play;
    public StudioEventEmitter BTN_Join;
    public StudioEventEmitter BTN_Launch;
    public StudioEventEmitter BTN_Esc;
    public StudioEventEmitter BTN_Instrument;
    public StudioEventEmitter MenuMusic;
    public StudioEventEmitter WinMusic;
    public StudioEventEmitter LevelMusic;

    override protected IEnumerator CoroutineStart() {
        isReady = true;

        yield return true;
    }
}