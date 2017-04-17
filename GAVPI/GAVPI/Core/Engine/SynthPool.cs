using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace GAVPI
{
    public class SynthPool
    {
        private List<SpeechSynthesizer> pool;
        private int current;
        private int max;

        public SynthPool()
        {
            current = 0;
            max = Properties.Settings.Default.ParallelSpeechPool;
            pool = new List<SpeechSynthesizer>();
            for (var i = 0; i < max; i++)
            {
                pool.Add(new SpeechSynthesizer());
            }
            foreach (SpeechSynthesizer s in pool)
            {
                s.SelectVoice(GAVPI.Settings.voice_info);
            }
        }

        public void Reload()
        {
            if (max == Properties.Settings.Default.ParallelSpeechPool) return;

            current = 0;
            max = Properties.Settings.Default.ParallelSpeechPool;
            pool.Clear();
            for (var i = 0; i < max; i++)
            {
                pool.Add(new SpeechSynthesizer());
            }
            foreach (SpeechSynthesizer s in pool)
            {
                s.SelectVoice(GAVPI.Settings.voice_info);
            }
        }

        public void Speak(string text)
        {
            pool[current++].SpeakAsync(text);
            current = current % max;
        }
    }
}
