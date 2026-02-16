namespace RentalSystem.Domain.ValueObjects
{
    public sealed class Address
    {
        public string AddressLine { get; private set; }
        public string City { get; private set; }
        public string Province { get; private set; }
        public string Country { get; private set; }

        private Address() { } // EF

        public Address(string addressLine, string city, string province, string country)
        {
            if (string.IsNullOrWhiteSpace(addressLine))
                throw new ArgumentException("AddressLine required");

            AddressLine = addressLine;
            City = city;
            Province = province;
            Country = country;
        }
    }

}
