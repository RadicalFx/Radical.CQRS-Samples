using FluentNHibernate.Mapping;
using Radical.CQRS;

namespace Sample.Domain.Data.Maps
{
    public class NHibernateDomainEventCommitMap : ClassMap<NHibernateDomainEventCommit>
    {
        public NHibernateDomainEventCommitMap()
        {
            Table( "DomainEventCommits" );
            Not.LazyLoad();
            Id( o => o.EventId ).GeneratedBy.Assigned();
            Map( o => o.AggregateId );
            Map( o => o.Version );
            Map( o => o.TransactionId );
            Map( o => o.PublishedOn );
            Map( o => o.EventType );
            Map( o => o.EventBlob );
            Map( o => o.StreamGroup );
            Map( o => o.IsDispatched );
        }
    }
}
