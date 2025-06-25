using System;
using Tao.Sdl;

namespace MyGame
{
    public class Music
    {
        private IntPtr music;
        public Music()
        {
            music = SdlMixer.Mix_LoadMUS("assets/Music/DeathByGlamour.wav");
            SdlMixer.Mix_PlayMusic(music, -1);
            SdlMixer.Mix_VolumeMusic(128);
        }
    }
}
