using SealSoundMaker.Util;
using SoundFlow.Abstracts;
using SoundFlow.Abstracts.Devices;
using SoundFlow.Backends.MiniAudio;
using SoundFlow.Components;
using SoundFlow.Enums;
using SoundFlow.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SealSoundMaker.Audio
{
    public static class Recording
    {
        private static AudioCaptureDevice device;
        private static Recorder recorder;

        public static bool Init(MiniAudioEngine engine)
        {
            try
            {
                //If no audio devices are available, show error
                if (engine.CaptureDevices.Length == 0)
                {
                    throw new Exception("No capture device available");
                }

                DeviceInfo deviceInfo = engine.CaptureDevices[0];

                device = engine.InitializeCaptureDevice(deviceInfo, AudioHandler.defaultFormat);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error($"Could not initialize Audio Engine for Recording: {ex.Message}");
                return false;
            }
        }

        public static void Start(string fileName)
        {
            //Check if audiohandler is initialized and stop and current recordings before starting a new one
            if (!AudioHandler.initialized) return;
            if (recorder != null && recorder.State != PlaybackState.Stopped) Stop();

            string outputFilePath = fileName;
            recorder = new Recorder(device, outputFilePath, "wav");

            device.Start();

            Result res = recorder.StartRecording();
            if (res.IsFailure)
            {
                Log.Error($"Failed to start recording: {res.Error?.Message}");
                device.Stop();
                return;
            }

            Log.Info("Started recording!");
        }

        public static void Stop()
        {
            if (!AudioHandler.initialized) return;

            Result res = recorder.StopRecording();
            device.Stop();

            if (res.IsFailure)
            {
                Log.Error($"Failed to stop recording: {res.Error?.Message}");
                return;
            }

            Log.Info("Stopped recording!");
        }
    }
}
