﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Nest;

namespace TIK.Persistance.ElasticSearch
{
 
    public abstract class EsRepository<T, TId> : BaseRepository
        where T : class, new()
    {

        private readonly string _indexName = "member";
        public EsRepository(IElasticClient elasticClient)
            :base(elasticClient)
        {

        }

        public string Save(T entry)
        {
            var result = Client.Index<T>(entry, c => c.Index(_indexName));
            return result.Id;
        }


        public bool Delete(TId id)
        {
            var result = Client.Delete<T>(id.ToString(),
                                          x => x.Index(_indexName));
            return result.Found;
        }

        public T Get(TId id)
        {
            var result = Client.Get<T>(id.ToString());
            return result.Source;
        }

        public IEnumerable<T> List()
        {
            var result = Client.Search<T>(search =>
                                          search.MatchAll());

            return result.Documents;
        }

        public IEnumerable<T> Search(IEnumerable<Tuple<Expression<Func<T, object>>, object>> paramValue)
        {
            SearchRequest req = new SearchRequest();
            var q = new QueryContainerDescriptor<T>();
            foreach (var item in paramValue)
            {
                q.Term(item.Item1, item.Item2);
            }
            req.Query = q;

            var result = Client.Search<T>(req);
            return result.Documents;
        }

    }
}
