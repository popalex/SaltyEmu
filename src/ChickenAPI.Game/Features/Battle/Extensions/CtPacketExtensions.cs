﻿using ChickenAPI.Core.ECS.Entities;
using ChickenAPI.Game.Data.TransferObjects.Skills;
using ChickenAPI.Game.Entities.Player;
using ChickenAPI.Packets.Game.Server.Battle;
using ChickenAPI.Enums.Game.Entity;
using ChickenAPI.Game.Entities.Monster;
using ChickenAPI.Game.Entities.Npc;

namespace ChickenAPI.Game.Features.Battle.Extensions
{
    public static class CtPacketExtensions
    {
        public static CtPacket GenerateCtPacket(IEntity entity, SkillDto skill)
        {
            var ct = new CtPacket();
            switch (entity)
            {
                case IPlayerEntity player:
                    ct.VisualType = VisualType.Character;
                    ct.VisualId = player.Id;
                    ct.SenderId = player.Id;
                    break;

                case INpcEntity npc:
                    ct.VisualType = VisualType.Npc;
                    ct.VisualId = npc.Id;
                    ct.SenderId = npc.Id;
                    break;

                case IMonsterEntity monster:
                    ct.VisualType = VisualType.Monster;
                    ct.VisualId = monster.Id;
                    ct.SenderId = monster.Id;
                    break;
            }
            ct.CastAnimationId = skill.CastAnimation;
            ct.CastEffect = skill.CastEffect;
            ct.SkillId = skill.Id;
            return ct;
        }
    }
}