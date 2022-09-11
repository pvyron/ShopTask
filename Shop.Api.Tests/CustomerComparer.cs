using Shop.Api.Models.DbModels;
using System.Collections;

namespace Shop.Api.Tests
{
    internal class CustomerComparer : IComparer, IComparer<Customer>
    {
        public int Compare(object expected, object actual)
        {
            var lhs = expected as Customer;
            var rhs = actual as Customer;

            if (lhs == null || rhs == null)
                throw new InvalidOperationException();

            return Compare(lhs, rhs);
        }

        public int Compare(Customer expected, Customer actual)
        {
            int temp;

            temp = expected.Id.CompareTo(actual.Id);

            if (temp != 0)
                return temp;

            temp = expected.FirstName.CompareTo(actual.FirstName);

            if (temp != 0)
                return temp;

            temp = expected.LastName.CompareTo(actual.LastName);

            if (temp != 0)
                return temp;

            temp = expected.Address?.CompareTo(actual.Address) ?? (-1) * actual.Address?.CompareTo(expected.Address) ?? 0;

            if (temp != 0)
                return temp;

            temp = expected.PostalCode?.CompareTo(actual.PostalCode) ?? (-1) * actual.PostalCode?.CompareTo(expected.PostalCode) ?? 0;

            return temp;
        }
    }
}
