using FluentNHibernate.Mapping;
using Sample.Domain.People;

namespace Sample.Domain.Data.Maps
{
    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Table( "People" );
            Not.LazyLoad();
            Id( p => p.Id ).GeneratedBy.Assigned();
            Map( p => p.Version ).OptimisticLock();
            Map( p => p.Name );
            Component( p => p.Info ).ColumnPrefix( "Info_" );
            HasMany<Address>( p => p.Addresses )
                .KeyColumn( "PersonId" )
                .Cascade.All()
                .Not.LazyLoad();
        }
    }

    class BornInfoMap : ComponentMap<BornInfo>
    {
        public BornInfoMap()
        {
            Map( b => b.Where );
            Map( b => b.When );
        }
    }

    class AddressMap : ClassMap<Address>
    {
        public AddressMap()
        {
            Table( "PersonAddresses" );
            Not.LazyLoad();
            Id( o => o.Id ).GeneratedBy.Assigned();
            Map( o => o.PersonId );
            Map( o => o.Street );
        }
    }
}
