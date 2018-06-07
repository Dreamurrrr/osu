﻿// Copyright (c) 2007-2018 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Colour;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Game.Graphics;
using osu.Game.Rulesets.UI.Scrolling;
using OpenTK.Graphics;

namespace osu.Game.Rulesets.Mania.UI.Components
{
    public class ColumnBackground : CompositeDrawable, IHasAccentColour
    {
        private readonly ScrollingDirection direction;

        public ColumnBackground(ScrollingDirection direction)
        {
            this.direction = direction;
        }

        private Box background;
        private Box backgroundOverlay;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new[]
            {
                background = new Box
                {
                    Name = "Background",
                    RelativeSizeAxes = Axes.Both,
                    Alpha = 0.3f
                },
                backgroundOverlay = new Box
                {
                    Name = "Background Gradient Overlay",
                    RelativeSizeAxes = Axes.Both,
                    Height = 0.5f,
                    Anchor = direction == ScrollingDirection.Up ? Anchor.TopLeft : Anchor.BottomLeft,
                    Origin = direction == ScrollingDirection.Up ? Anchor.TopLeft : Anchor.BottomLeft,
                    Blending = BlendingMode.Additive,
                    Alpha = 0
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            updateColours();
        }

        private Color4 accentColour;

        public Color4 AccentColour
        {
            get => accentColour;
            set
            {
                if (accentColour == value)
                    return;
                accentColour = value;

                updateColours();
            }
        }

        private void updateColours()
        {
            if (!IsLoaded)
                return;

            background.Colour = AccentColour;

            var brightPoint = AccentColour.Opacity(0.6f);
            var dimPoint = AccentColour.Opacity(0);

            backgroundOverlay.Colour = ColourInfo.GradientVertical(
                direction == ScrollingDirection.Up ? brightPoint : dimPoint,
                direction == ScrollingDirection.Up ? dimPoint : brightPoint);
        }

        private bool isLit;

        /// <summary>
        /// Whether the column lighting should be visible.
        /// </summary>
        public bool IsLit
        {
            set
            {
                if (isLit == value)
                    return;
                isLit = value;

                if (isLit)
                    backgroundOverlay.FadeTo(1, 50, Easing.OutQuint).Then().FadeTo(0.5f, 250, Easing.OutQuint);
                else
                    backgroundOverlay.FadeTo(0, 250, Easing.OutQuint);
            }
        }

    }
}
