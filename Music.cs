using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tao.Sdl;

namespace MyGame
{
    public class Music
    {
        private IntPtr music;
        public Music()
        {
            music = SdlMixer.Mix_LoadMUS("assets/Music/DeathByGlamour.wav");
            //sfx= SdlMixer.Mix_LoadWAV(); //for sound efects.
            SdlMixer.Mix_PlayMusic(music, -1);
            //SdlMixer.Mix_PlayChannel(int channel, sfx, -1 for loop & 0? for one time track); for playing sound effects.
            SdlMixer.Mix_VolumeMusic(128);
            //if (music == IntPtr.Zero)
            //{
            //    Engine.Debug($"Music failed to load: {Sdl.SDL_GetError()}");
            //}
            //else
            //{
            //    SdlMixer.Mix_PlayMusic(music, -1);
            //    Engine.Debug((SdlMixer.Mix_PlayingMusic()).ToString());
            //} //Check if it's imported right. 
        }
    }
}
