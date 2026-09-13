using Microsoft.Extensions.Logging;
using SealSoundMaker.Util;
using SoundFlow.Abstracts.Devices;
using SoundFlow.Backends.MiniAudio;
using SoundFlow.Components;
using SoundFlow.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace SealSoundMaker.Audio
{
    public static class AudioHandler
    {
        private static MiniAudioEngine audioEngine = new MiniAudioEngine();

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
            bool initRecording = Recording.Init(audioEngine);

            if (initRecording)
            {
                initialized = true;
                Log.Info("Successfully initialized Audio Engine!");
            }
        }
    }
}
