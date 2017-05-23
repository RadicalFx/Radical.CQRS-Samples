using System;

namespace Sample.Domain.People
{
    internal class Address
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public String Street { get; set; }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals( object obj )
        {
            var other = obj as Address;
            if( other != null ) 
            {
                return other.Id == this.Id;
            }

            return false;
        }
    }
}
