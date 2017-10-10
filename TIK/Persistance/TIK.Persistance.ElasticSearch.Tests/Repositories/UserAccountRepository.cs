﻿using System;
using Nest;
using TIK.Domain.Member;
using TIK.Persistance.ElasticSearch;

namespace TIK.Persistance.ElasticSearch.Tests.Repositories
{
    public class UserAccountRepository : EsRepository<UserAccount, Guid>
    {
        public UserAccountRepository(IElasticClient elasticClient)
            :base(elasticClient)
        {
        }
    }
}
