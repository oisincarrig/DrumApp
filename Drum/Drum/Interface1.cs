using System.IO;
using System;


namespace SimpleAudio
{
    public interface ISimpleAudioPlayer
    {
        bool Load(Stream audioStream);

        void Play();

        void Pause();

        void Stop();
    }
}
