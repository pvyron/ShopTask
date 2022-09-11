using Shop.Api.Models.DbModels;
using Shop.Api.Models.RequestModels;
using Shop.Api.Models.ResponseModels;
using System.Collections;

namespace Shop.Api.Tests
{
    internal class CustomerApiComparer : IComparer //, IComparer<Customer>
    {
        public int Compare(object expected, object actual)
        {
            var lhs = expected as Customer;
            var rhs = actual as CustomerResponseModel;

            if (lhs == null || rhs == null)
                throw new InvalidOperationException();

            return Compare(lhs, rhs);
        }

        public int Compare(Customer expected, CustomerResponseModel actual)
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

        public int Compare(CustomerRequestModel expected, CustomerResponseModel actual)
        {
            int temp;

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
