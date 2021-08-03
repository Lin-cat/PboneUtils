﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;
using Terraria.GameInput;
using Terraria.GameContent.UI.Elements;

namespace PboneUtils.UI.Elements
{
    public class UIMySearchBar : UIElement
    {
        public const int CharacterLimit = 16;
        public const string HintText = "Search...";

        public bool IsEmpty => string.IsNullOrEmpty(Text);

        public UIPanel BackPanel;
        public string Text;
        public bool Focused;
        public int CursorBlinkTimer;
        public bool ShowCursorBlink;

        public delegate void TextChangeDelegate(string oldText, string currentText);
        public event TextChangeDelegate OnTextChange;

        public UIMySearchBar(int width, int height)
        {
            Width.Set(width, 0f);
            Height.Set(height, 0f);

            BackPanel = new UIPanel();
            BackPanel.Width.Set(width, 0f);
            BackPanel.Height.Set(height, 0f);
            BackPanel.BackgroundColor = new Color(22, 25, 55);
            BackPanel.PaddingLeft = BackPanel.PaddingRight = BackPanel.PaddingTop = BackPanel.PaddingBottom = 0;
            Append(BackPanel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (PlayerInput.Triggers.Current.MouseLeft)
            {
                if (ContainsPoint(Main.MouseScreen))
                {
                    Main.clrInput();
                    Focused = true;
                }
                else
                {
                    Focused = false;
                }
            }

            if (PlayerInput.Triggers.Current.Inventory)
            {
                Focused = false;
            }
        }

        protected override void DrawChildren(SpriteBatch spriteBatch)
        {
            base.DrawChildren(spriteBatch);

            PlayerInput.WritingText = Focused;
            Main.LocalPlayer.mouseInterface = Focused;
            if (Focused)
            {
                Main.instance.HandleIME();

                string newInput = Main.GetInputText(Text);
                if (newInput != Text)
                {
                    OnTextChange?.Invoke(Text, newInput);
                    Text = newInput;
                }
            }

            Vector2 position = GetDimensions().Position() + new Vector2(6, 4);
            string displayText = Text ?? "";

            if (string.IsNullOrEmpty(displayText) && !Focused)
            {
                Utils.DrawBorderString(spriteBatch, HintText, position, Color.DarkGray);
            }

            if (Focused && ++CursorBlinkTimer >= 20)
            {
                ShowCursorBlink = !ShowCursorBlink;
                CursorBlinkTimer = 0;
            }

            if (Focused && ShowCursorBlink)
            {
                displayText += "|";
            }

            Utils.DrawBorderString(spriteBatch, displayText, position, Color.White);
        }
    }
}
