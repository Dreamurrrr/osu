﻿using System;
using System.Collections.Generic;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Graphics;
using osu.Framework.Testing;
using osu.Game.Graphics;
using osu.Game.Overlays.Volume;

namespace osu.Game.Tests.Visual
{
    public class TestCaseVolumeControl : TestCase
    {
        public override IReadOnlyList<Type> RequiredTypes => new[] { typeof(VolumeMeter), typeof(MuteButton) };

        [BackgroundDependencyLoader]
        private void load(AudioManager audio, OsuColour colours)
        {
            VolumeMeter meter;
            Add(meter = new VolumeMeter("MASTER", 125, colours.PinkDarker));
            Add(new MuteButton
            {
                Margin = new MarginPadding { Top = 200 }
            });

            meter.Bindable.BindTo(audio.Volume);
        }
    }
}
