using SimpleAudio;
using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace Drum
{
    public partial class MainPage : ContentPage
    {
        enum DrumType
        {
            Tom,
            Snare,
            Kick,
            HiHat,
            count
        }

        ISimpleAudioPlayer[] players = new ISimpleAudioPlayer[(int)DrumType.count];
        Animation[] animations = new Animation[(int)DrumType.count];

        public MainPage()
        {
            //for (int i = 0; i < (int)DrumType.count; i++)
            //{
               // players[i] = Drum.App.CreateAudioPlayer();
            //}

            InitializeComponent();

            Color colorButton = btnPlayKick.BackgroundColor;
            Color colorHighlight = Color.FromHex("#EF5A56");

            animations[(int)DrumType.Kick] = new Animation(v => btnPlayKick.BackgroundColor = GetBlendedColor(colorButton, colorHighlight, v), 0, 1);
            animations[(int)DrumType.HiHat] = new Animation(v => btnPlayHiHat.BackgroundColor = GetBlendedColor(colorButton, colorHighlight, v), 0, 1);
            animations[(int)DrumType.Snare] = new Animation(v => btnPlaySnare.BackgroundColor = GetBlendedColor(colorButton, colorHighlight, v), 0, 1);
            animations[(int)DrumType.Tom] = new Animation(v => btnPlayTom.BackgroundColor = GetBlendedColor(colorButton, colorHighlight, v), 0, 1);

            btnPlayKick.Clicked += (s, e) => OnDrumButton(DrumType.Kick);
            btnPlaySnare.Clicked += (s, e) => OnDrumButton(DrumType.Snare);
            btnPlayTom.Clicked += (s, e) => OnDrumButton(DrumType.Tom);
            btnPlayHiHat.Clicked += (s, e) => OnDrumButton(DrumType.HiHat);
        }

        void PickerKitsSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;

            LoadSamples(picker.SelectedIndex + 1);
        }

        void OnDrumButton(DrumType drumType)
        {
            players[(int)drumType]?.Play();
            animations[(int)drumType]?.Commit(this, drumType.ToString());
        }

        void LoadSamples(int index)
        {
            if (index < 1 || index > 2)
                return;

            players[(int)DrumType.Kick].Load(GetStreamFromFile($"Audio.bd{index}.wav"));
            players[(int)DrumType.Snare].Load(GetStreamFromFile($"Audio.sd{index}.wav"));
            players[(int)DrumType.Tom].Load(GetStreamFromFile($"Audio.tt{index}.wav"));
            players[(int)DrumType.HiHat].Load(GetStreamFromFile($"Audio.hh{index}.wav"));
        }

        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("DrumPad." + filename);

            return stream;
        }

        Color GetBlendedColor(Color color1, Color color2, double percentage)
        {
            return new Color(percentage * color1.R + (1 - percentage) * color2.R,
                             percentage * color1.G + (1 - percentage) * color2.G,
                             percentage * color1.B + (1 - percentage) * color2.B);
        }
    }
}
