﻿using AutoMapper;
using ChickenAPI.Data.AccessLayer.Skill;
using ChickenAPI.Data.TransferObjects.Skills;
using NosSharp.DatabasePlugin.Context;
using NosSharp.DatabasePlugin.Models.Skill;
using NosSharp.DatabasePlugin.Services.Base;

namespace NosSharp.DatabasePlugin.Services.Skill
{
    public class SkillDao : MappedRepositoryBase<SkillDto, SkillModel>, ISkillService
    {
        public SkillDao(NosSharpContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}