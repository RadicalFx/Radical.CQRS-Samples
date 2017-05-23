using FluentNHibernate.Mapping;
using Sample.Domain.Companies;

namespace Sample.Domain.Data.Maps
{
    public class CompanyStateMap : ClassMap<Company.State>
    {
        public CompanyStateMap()
        {
            Table( "Companies" );
            Not.LazyLoad();
            Id( s => s.Id ).GeneratedBy.Assigned();
            Map( s => s.Version ).OptimisticLock();
            Map( s => s.Name );
        }
    }
}
