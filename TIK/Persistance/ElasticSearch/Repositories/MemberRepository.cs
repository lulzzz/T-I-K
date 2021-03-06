﻿using System;
using Nest;
using TIK.Domain.Membership;

namespace TIK.Persistance.ElasticSearch.Repositories
{
    public class MemberRepository: EsRepository<Member, Int32>, IMemberRepository
    {
        public MemberRepository(EsContext context) : base(context)
        {
        }
    }
}
