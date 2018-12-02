﻿using System.Collections.Generic;
using ChickenAPI.Data.Item;
using ChickenAPI.Data.Shop;
using ChickenAPI.Game.Events;

namespace ChickenAPI.Game.Shops.Events
{
    public class ShopPlayerShopCreateEvent : ChickenEventArgs
    {
        public List<PersonalShopItem> ShopItems { get; set; }

        public string Name { get; set; }
    }
}