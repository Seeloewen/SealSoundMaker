using SealSoundMaker.Util;
using SoundFlow.Backends.MiniAudio;
using SoundFlow.Structs;

namespace SealSoundMaker.Audio
{
    public static class AudioHandler
    {
        public static MiniAudioEngine audioEngine = new MiniAudioEngine();

        public static bool initialized;

        public static readonly AudioFormat defaultFormat = new AudioFormat()
        {
            Format = SoundFlow.Enums.SampleFormat.F32,
            SampleRate = 48000,
            Channels = 1,
        };

        public static void Init()
        {
            Log.Info("Initializing Audio Engine...");
            bool initRecording = Recording.Init();
            bool initPlayback = Playback.Init();

            if (initRecording && initPlayback)
            {
                initialized = true;
                Log.Info("Successfully initialized Audio Engine!");
            }
        }
    }
}
