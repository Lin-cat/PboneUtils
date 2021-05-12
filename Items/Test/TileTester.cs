﻿using PboneUtils.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace PboneUtils.Items.Test
{
    public class TileTester : PItem
    {
        public override string Texture => "PboneUtils/Items/Test/TestItem";

        public override void SetDefaults()
        {
            base.SetDefaults();
            UseTime = 30;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.createTile = ModContent.TileType<PetrifiedSafeTile>();
        }
    }
}
