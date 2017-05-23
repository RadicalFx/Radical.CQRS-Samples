using Radical.CQRS;
using Sample.Domain.People;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Radical.CQRS.Data;

namespace Sample.Domain.Services
{
    class PersonFinder : IAggregateFinder<DomainContext, Person>
    {
        public Person FindById(DomainContext session, AggregateQuery aggregateQuery)
        {
            //ignoring Aggregate Version
            var db = session.Set<Person>();
            var aggregate = db.Include(p => p.Addresses)
                .Single(p => p.Id == aggregateQuery.Id);

            return aggregate;
        }

        public IEnumerable<Person> FindById(DomainContext session, params AggregateQuery[] aggregateQueries)
        {
            //ignoring Aggregate Version
            var aggregateIds = aggregateQueries.Select(q => q.Id).ToArray();
            var db = session.Set<Person>();
            var results = db.Include(p => p.Addresses)
                .Where(p => aggregateIds.Contains(p.Id))
                .ToList();

            return results;
        }
    }
}
