﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChickenAPI.Data.AccessLayer.Repository;
using ChickenAPI.Enums.Game.Buffs;
using NosSharp.DatabasePlugin.Models.BCard;

namespace NosSharp.DatabasePlugin.Models
{
    [Table("_data_card")]
    public class CardModel : IMappedDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        public int Duration { get; set; }

        public int EffectId { get; set; }

        public byte Level { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public short TimeoutBuff { get; set; }

        public BuffType BuffType { get; set; }

        public byte TimeoutBuffChance { get; set; }

        public int Delay { get; set; }

        public byte Propability { get; set; }
        public IEnumerable<CardBCardModel> BCards { get; set; }
    }
}