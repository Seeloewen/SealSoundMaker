using SealSoundMaker.Util;
using SoundFlow.Abstracts.Devices;
using SoundFlow.Backends.MiniAudio;
using SoundFlow.Components;
using SoundFlow.Providers;
using SoundFlow.Structs;
using System;
using System.IO;


namespace SealSoundMaker.Audio
{
    internal class Playback
    {
        private static AudioPlaybackDevice device;
        private static SoundPlayer player;

        public static bool Init()
        {
            MiniAudioEngine engine = AudioHandler.audioEngine;

            try
            {
                //If no audio devices are available, show error
                if (engine.CaptureDevices.Length == 0)
                {
                    throw new Exception("No playback device available");
                }

                DeviceInfo deviceInfo = engine.PlaybackDevices[0];

                device = engine.InitializePlaybackDevice(deviceInfo, AudioHandler.defaultFormat);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Could not initialize Audio Engine for Playback: {ex.Message}");
                return false;
            }
        }

        public static void Play(string path)
        {
            StreamDataProvider data = new StreamDataProvider(AudioHandler.audioEngine, AudioHandler.defaultFormat, File.OpenRead(path));

            player = new SoundPlayer(AudioHandler.audioEngine, AudioHandler.defaultFormat, data);

            device.MasterMixer.AddComponent(player);
            device.Start();
            player.Play();

            Log.Info("Starting playback!");
        }

        public static void Stop()
        {
            device.Stop();
            player.Stop();

            Log.Info("Stopped playback!");
        }
    }
}
